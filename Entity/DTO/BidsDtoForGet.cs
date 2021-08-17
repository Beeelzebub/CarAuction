using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;

namespace Entity.DTO
{
    public class BidsDtoForGet
    {

        public BidStatus BidStatus { get; set; }
        public int LotId { get; set; }
    }
}
