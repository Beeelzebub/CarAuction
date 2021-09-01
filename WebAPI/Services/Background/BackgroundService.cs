﻿using System;
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
            var lot =  _repository.Lot.Get(lotId);

            if (lot == null)
            {
                return;
            }

            lot.Status = LotStatus.Ended;

            var winningBid =  _repository.Bid.GetActiveBid(lotId);

            if (winningBid == null)
            {
                _repository.Bid.Save();
                return;
            }

            winningBid.BidStatus = BidStatus.Won;

            _repository.Bid.Save();
        }
    }
}