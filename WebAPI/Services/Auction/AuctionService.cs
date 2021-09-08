using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Entity;
using Entity.Models;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Repositories;
using Services.Exceptions;

namespace Services.Auction
{
    public class AuctionService : IAuctionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;

        public AuctionService(IRepositoryManager repositoryManager, UserManager<User> userManager)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }

        public async Task BidAsync(int lotId, ClaimsPrincipal bidderClaims)
        {
            var bidderId = _userManager.GetUserId(bidderClaims);

            var lot = await _repositoryManager.Lot.GetAsync(lotId);

            if (lot == null)
            {
                throw new NotFoundException($"Lot with id {lotId} is not found");
            }

            var activeBid = _repositoryManager.Bid.GetActiveBid(lotId);

            if (activeBid != null)
            {
                if (activeBid.BuyerId == bidderId)
                {
                    throw new BadRequestException("You have already placed a bet");
                }

                activeBid.BidStatus = BidStatus.Outbid;
            }

            lot.CurrentCost += lot.MinimalStep;

            var newBid = new Bid
            {
                LotId = lot.Id,
                BuyerId = bidderId
            };

            await _repositoryManager.Bid.CreateAsync(newBid);

            await _repositoryManager.SaveAsync();
        }

        public async Task ChangeLotStatus(int lotId, LotStatus status)
        {
            var lot = await _repositoryManager.Lot.GetAsync(lotId);

            if (lot == null)
            {
                throw new NotFoundException($"Lot with id {lotId} is not found");
            }

            lot.Status = status;

            if (lot.Status == LotStatus.Approved)
            {
                lot.StartDate = DateTime.Now;
                lot.EndDate = DateTime.Now.AddMinutes(5);
                BackgroundJob.Schedule(() => ChooseWinner(lotId), TimeSpan.FromMinutes(5));
            }

            await _repositoryManager.SaveAsync();
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
                _repositoryManager.Bid.Save();
                return;
            }

            winningBid.BidStatus = BidStatus.Won;

            _repositoryManager.Bid.Save();
        }
    }
}
