using Entity.Models;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class LotCreationDto
    {
        [Range(1900, 2021, ErrorMessage = "Year failed")]
        public int Year { get; set; }
        [Required(ErrorMessage = "ImageFailed")]
        public string ImageUrl { get; set; }
        public Fuel Fuel { get; set; }

        public CarBody CarBody { get; set; }
        public DriveUnit DriveUnit { get; set; }
        
        public int ModelId { get; set; }
        public int BrandId { get; set; }
        public decimal MinimalStep { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal RedemptionPrice { get; set; }
    }
}
