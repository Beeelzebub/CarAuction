using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public Fuel Fuel { get; set; }
        
        public CarBody CarBody { get; set; }
        public DriveUnit DriveUnit { get; set; }
        
        public virtual Model Model { get; set; }

        public Guid ModelId { get; set; }
        public virtual Lot Lot { get; set; }
        public Guid LotId { get; set; }

        


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
    public enum DriveUnit
    {
        FrontWheelDrive,
        RearDrive,
        FourWheelDrive
    }
}
