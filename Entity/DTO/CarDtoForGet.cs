using Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.DTO
{
    public class CarDtoForGet
    {
        
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public Fuel Fuel { get; set; }

        

        public CarBody CarBody { get; set; }
        public DriveUnit DriveUnit { get; set; }
        
    }
}
