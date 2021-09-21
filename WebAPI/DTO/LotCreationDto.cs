using System.ComponentModel.DataAnnotations;
using Enums;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class LotCreationDto
    {
        [Range(1900, 2021, ErrorMessage = "Year failed")]
        public int Year { get; set; }
        public IFormFile Image { get; set; }
        public Fuel Fuel { get; set; }

        public CarBody CarBody { get; set; }
        public DriveUnit DriveUnit { get; set; }
        
        public string Name { get; set; }
        public string BrandName { get; set; }
        public decimal MinimalStep { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal RedemptionPrice { get; set; }
    }
}
