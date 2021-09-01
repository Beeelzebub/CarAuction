using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CarAuctionContext _context;
        private readonly IMapper _mapper;
        private ICarRepository _carRepository;
        private ILotRepository _lotRepository;
        private IBidRepository _bidRepository;

        public RepositoryManager(CarAuctionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEntityRepository<TEntity> GetRepositoryByEntity<TEntity>() where TEntity : class, IEntity, new() =>
            new EntityRepository<TEntity>(_context);

        public ICarRepository Car => 
            _carRepository ?? (_carRepository = new CarRepository(_context, _mapper));

        public ILotRepository Lot =>
            _lotRepository ?? (_lotRepository = new LotRepository(_context));

        public IBidRepository Bid =>
            _bidRepository ?? (_bidRepository = new BidRepository(_context));
    }
}
