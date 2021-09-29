using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using DTO.Response;
using Entity.Models;
using Entity.RequestFeatures;
using Enums;
using Microsoft.AspNetCore.Identity;
using Repositories;

namespace Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ProfileService(IRepositoryManager repositoryManager, UserManager<User> userManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        
        public async Task<BaseResponse> RemoveLotAsync(int id, ClaimsPrincipal sellerClaims)
        {
            var car = await _repositoryManager.Car.GetCarAsync(id);

            var checkResult = LotCheck(car, sellerClaims);

            if (checkResult != ErrorCode.Success)
            {
                return BaseResponse.Fail(checkResult);
            }

            _repositoryManager.Car.Delete(car);

            await _repositoryManager.SaveAsync();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> AddLotAsync(LotCreationDto lotCreationDto, ClaimsPrincipal sellerClaims)
        {
            

            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(lotCreationDto.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)lotCreationDto.Image.Length);
            }
            var car = new Car
            {
                Image = imageData,
                Year = lotCreationDto.Year,
                Fuel = lotCreationDto.Fuel,
                CarBody = lotCreationDto.CarBody,
                DriveUnit = lotCreationDto.DriveUnit
            };
            
            var lot = _mapper.Map<Lot>(lotCreationDto);
            //var model = _mapper.Map<Model>(lotCreationDto);
            //var brand = _mapper.Map<Brand>(lotCreationDto);
            var models = await _repositoryManager.GetRepositoryByEntity<Model>().GetListAsync();
            var brands = await _repositoryManager.GetRepositoryByEntity<Brand>().GetListAsync();
            lot.SellerId = _userManager.GetUserId(sellerClaims);
            lot.Car = car;
            foreach (var data in models)
            {
                if (data.Name == lotCreationDto.Name)
                {
                    lot.Car.Model = data;
                }
            }
            lot.Car.Model ??= new Model
            {
                Name = lotCreationDto.Name
            };
            foreach (var data in brands)
            {
                if (data.BrandName == lotCreationDto.BrandName)
                {
                    lot.Car.Model.Brand = data;
                }
            }
            lot.Car.Model.Brand ??= new Brand
            {
                BrandName = lotCreationDto.BrandName
            };
            //lot.Car.Model = model;
            //lot.Car.Model.Brand = brand;

            await _repositoryManager.Lot.CreateAsync(lot);

            await _repositoryManager.SaveAsync();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> GetUsersCarInfoAsync(int id, ClaimsPrincipal userClaims)
        {
            var car = await _repositoryManager.Car.GetCarAsync(id);

            var checkResult = LotCheck(car, userClaims);

            if (checkResult != ErrorCode.Success)
            {
                return BaseResponse.Fail(checkResult);
            }

            var carDto = _mapper.Map<GetOneCarDto>(car);
            _mapper.Map(car.Lot, carDto);

            return BaseResponse.Success(carDto);
        }

        public async Task<BaseResponse> GetUsersBidsAsync(ClaimsPrincipal userClaims)
        {
            var userId = _userManager.GetUserId(userClaims);

            var bids = await _repositoryManager.Bid.GetBidsByUserAsync(userId);

            var bidsDto = _mapper.Map<List<BidsDto>>(bids);

            return BaseResponse.Success(bidsDto);
        }

        public async Task<BaseResponse> GetUsersCarsAsync(CarsParametersInProfile carsParametersInProfile, ClaimsPrincipal userClaims)
        {
            var userId = _userManager.GetUserId(userClaims);

            var cars = await _repositoryManager.Car.GetListByParametersAsync(userId, carsParametersInProfile);

            var carDtoList = _mapper.Map<PagedList<CarDto>>(cars);

            return BaseResponse.Success(carDtoList);
        }
        


        private ErrorCode LotCheck(Car car, ClaimsPrincipal userClaims)
        {
            var userId = _userManager.GetUserId(userClaims);

            if (car == null)
            {
                return ErrorCode.LotNotFoundError;
            }

            if (car.Lot.SellerId != userId)
            {
                return ErrorCode.NoPermissionsError;
            }

            return ErrorCode.Success;
        }
    }
}
