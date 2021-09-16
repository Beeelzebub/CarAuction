using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GetOneCarDto
    {
        public int Id { get; set; }


        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public string Fuel { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }

        public string CarBody { get; set; }
        public string DriveUnit { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MinimalStep { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal CurrentCost { get; set; }
        public decimal RedemptionPrice { get; set; }
    }
}
