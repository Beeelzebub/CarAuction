using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTO
{
    public class CarDtoForCreation
    {
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public Fuel Fuel { get; set; }

        public CarBody CarBody { get; set; }
        public DriveUnit DriveUnit { get; set; }
        

        public int ModelId { get; set; }
        public Lot Lot { get; set; }
    }
}
