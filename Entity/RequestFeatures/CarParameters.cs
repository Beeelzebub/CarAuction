using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.RequestFeatures
{
    public class CarParameters: RequestParameters
    {
        public uint MinYear { get; set; }
        public uint MaxYear { get; set; } = int.MaxValue;
        public bool ValidYearRange => MaxYear > MinYear;
        public string Model { get; set; }
        public string Brand { get; set; }
    }
}
