using System;
using Contracts;
using Contracts.Services;
using Entity.Models;
using Repositories;

namespace Services
{
    public class BackgroundService : IBackgroundService
    {
        private readonly ICarRepository _carRepository;

        public BackgroundService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public void ChooseWinner(int lotId)
        {
            var lot = _carRepository.GetLot(lotId);

            if (lot == null)
            {
                return;
            }

            lot.Status = Status.Ended;

            var winningBid = _carRepository.GetActiveBid(lotId);

            if (winningBid == null)
            {
                _carRepository.Save();
                return;
            }

            winningBid.BidStatus = BidStatus.Won;

            _carRepository.Save();
        }
    }
}
