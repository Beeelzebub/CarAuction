using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryManager
    {
        private ICarRepository _carRepository;
        private ILotRepository _lotRepository;
        private IBidRepository _bidRepository;
    }
}
