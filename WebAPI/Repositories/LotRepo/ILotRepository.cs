﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Entity.Models;

namespace Repositories
{
    public interface ILotRepository : IRepositoryBase<Lot>
    {
        public Task<List<Lot>> GetLotsByStatusAsync(LotStatus status);

        public Task AddLot(CarDtoForCreation carDtoForCreation, string userId);
    }
}
