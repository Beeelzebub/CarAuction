using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
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

        public async Task ChangeLotStatusAsync(int lotId, JsonPatchDocument<Lot> jsonPatch)
        {
            var lot = await _repositoryManager.Lot.GetAsync(lotId);

            if (lot == null)
            {
                throw new NotFoundException($"Lot with id {lotId} is not found");
            }

            jsonPatch.ApplyTo(lot);

            if (lot.Status == LotStatus.Approved)
            {
                lot.StartDate = DateTime.Now;
                lot.EndDate = DateTime.Now.AddMinutes(5);
                BackgroundJob.Schedule(() => ChooseWinner(lotId), TimeSpan.FromMinutes(5));
            }

            await _repositoryManager.SaveAsync();
        }

        public async Task<List<CarDto>> GetPendingCarsAsync()
        {
            var cars = await _repositoryManager.Car.GetCarsByStatusAsync(LotStatus.Pending);

            return _mapper.Map<List<CarDto>>(cars);
        }

        public async Task<CarDto> GetPendingCarAsync(int id)
        {
            var car = await _repositoryManager.Car.GetAsync(id);

            if (car == null || car.Lot.Status != LotStatus.Pending)
            {
                throw new NotFoundException("Car is not found");
            }

            return _mapper.Map<CarDto>(car);
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
