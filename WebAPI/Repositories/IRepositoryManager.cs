using System;
using System.Collections.Generic;
using System.Text;
using Repositories;

namespace Repositories
{
    public interface IRepositoryManager
    {
        ICarRepository Car { get; }
        ILotRepository Lot { get; }
        IBidRepository Bid { get; }
    }
}
