using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        public int Year { get; set; }
        public Brand Brand { get; set; }
        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        public Fuel Fuel { get; set; }
        public CarBody CarBody { get; set; }
        public Model Model { get; set; }
        [ForeignKey("Model")]
        public Guid ModelId { get; set; }
        public DriverUnit DriverUnit { get; set; }

    }
    public enum Fuel
    {
        Gas,
        Petrol,
        Diesel,
        Electric
    }

    public enum CarBody
    {
        PickupTrack,
        Universal,
        Sedan,
        Coupe,
        Hatchback,
        Minivan
    }
    public enum DriverUnit
    {
        FrontWheelDrive,
        RearDrive,
        FourWheelDrive
    }
}
