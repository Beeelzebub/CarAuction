using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;

namespace Entity.RequestFeatures
{
    public class CarsParametersInProfile: RequestParameters
    {
        public LotStatus? Status { get; set; }
    }
}
