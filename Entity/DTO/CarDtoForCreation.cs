using Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.DTO
{
    public class CarDtoForCreation
    {
        [Range(1900, 2021, ErrorMessage = "Year failed")]
        public int Year { get; set; }
        [Required(ErrorMessage = "ImageFailed")]
        public string ImageUrl { get; set; }
        public Fuel Fuel { get; set; }

        public CarBody CarBody { get; set; }
        public DriveUnit DriveUnit { get; set; }
        
        public string Model { get; set; }
        public string Brand { get; set; }
        public decimal MinimalStep { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal RedemptionPrice { get; set; }
    }
}
