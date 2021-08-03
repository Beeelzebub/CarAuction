using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        public int Year { get; set; }
        public Brand Brand { get; set; }
        public Guid BrandId { get; set; }
        public Fuel Fuel { get; set; }
        public Guid FuelId { get; set; }
        public CarBody CarBody { get; set; }
        public Guid CarBodyId { get; set; }
        public Model Model { get; set; }
        public Guid ModelId { get; set; }
        public DriveUnit DriverUnit { get; set; }
        public Guid DriverUnitId { get; set; }

    }
}
