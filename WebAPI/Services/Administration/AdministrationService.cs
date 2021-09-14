using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using DTO.Response;
using Entity.Models;
using Enums;
using Hangfire;
using Microsoft.AspNetCore.JsonPatch;
using Repositories;
using Services.Exceptions;

namespace Services.Administration
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AdministrationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<BaseResponse> ChangeLotStatusAsync(int lotId, JsonPatchDocument<Lot> jsonPatch)
        {
            var lot = await _repositoryManager.Lot.GetAsync(lotId);

            if (lot == null)
            {
                return BaseResponse.Fail(ErrorCode.LotNotFoundError);
            }

            jsonPatch.ApplyTo(lot);

            if (lot.Status == LotStatus.Approved)
            {
                lot.StartDate = DateTime.Now;
                lot.EndDate = DateTime.Now.AddMinutes(5);
                BackgroundJob.Schedule(() => ChooseWinner(lotId), TimeSpan.FromMinutes(5));
            }

            await _repositoryManager.SaveAsync();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> GetPendingCarsAsync()
        {
            var cars = await _repositoryManager.Car.GetCarsByStatusAsync(LotStatus.Pending);

            var carDtoList = _mapper.Map<List<CarDto>>(cars);

            return BaseResponse.Success(carDtoList);
        }

        public async Task<BaseResponse> GetPendingCarAsync(int id)
        {
            var car = await _repositoryManager.Car.GetAsync(id);

            if (car == null || car.Lot.Status != LotStatus.Pending)
            {
                return BaseResponse.Fail(ErrorCode.CarNotFound);
            }

            var carDto = _mapper.Map<CarDto>(car);

            return BaseResponse.Success(carDto);
        }

        public void ChooseWinner(int lotId)
        {
            var lot = _repositoryManager.Lot.Get(lotId);

            if (lot == null)
            {
                return;
            }

            lot.Status = LotStatus.Ended;

            var winningBid = _repositoryManager.Bid.GetActiveBid(lotId);

            if (winningBid == null)
            {
                _repositoryManager.Save();
                return;
            }

            winningBid.BidStatus = BidStatus.Won;

            _repositoryManager.Save();
        }
    }
}
