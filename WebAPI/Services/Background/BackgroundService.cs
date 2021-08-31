using System;
using Entity.Models;
using Repositories;

namespace Services.Background
{
    public class BackgroundService : IBackgroundService
    {
        private readonly IRepositoryManager _repository;

        public BackgroundService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public void ChooseWinner(int lotId)
        {
            var lot = _repository.Car.GetLot(lotId);

            if (lot == null)
            {
                return;
            }

            lot.Status = Status.Ended;

            var winningBid = _repository.Car.GetActiveBid(lotId);

            if (winningBid == null)
            {
                _repository.Car.Save();
                return;
            }

            winningBid.BidStatus = BidStatus.Won;

            _repository.Car.Save();
        }
    }
}
