using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryManager
    {
        public IAdminRepository Admin { get; }
        public IProfileRepository Profile { get; }
        public ICarRepository Car { get; }
    }
}
