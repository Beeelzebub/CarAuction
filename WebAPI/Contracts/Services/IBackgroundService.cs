using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IBackgroundService
    {
        void ChooseWinner(int lotId);
    }
}
