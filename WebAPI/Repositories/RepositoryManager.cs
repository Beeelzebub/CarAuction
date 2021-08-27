using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Contracts;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CarAuctionContext _context;
        private readonly IMapper _mapper;
        private IAdminRepository _adminRepository;
        private ICarRepository _carRepository;
        private IProfileRepository _profileRepository;

        public RepositoryManager(CarAuctionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICarRepository Car => 
            _carRepository ?? (_carRepository = new CarRepository(_context));

        public IProfileRepository Profile => 
            _profileRepository ?? (_profileRepository = new ProfileRepository(_context, _mapper));

        public IAdminRepository Admin =>
            _adminRepository ?? (_adminRepository = new AdminRepository(_context));
    }
}
