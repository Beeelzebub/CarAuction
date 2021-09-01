using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Background
{
    public interface IBackgroundService
    {
        void ChooseWinner(int lotId);
    }
}
