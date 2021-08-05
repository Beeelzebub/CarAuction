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

        [Required(ErrorMessage = "Year is a required field.")]
        [MinLength(4, ErrorMessage = "Incorrect Year.")]
        [MaxLength(4, ErrorMessage = "Incorrect Year.")]
        public int Year { get; set; }

        public Brand Brand { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        [Required(ErrorMessage = "Fuel is a required field.")]
        public Fuel Fuel { get; set; }

        [Required(ErrorMessage = "Car Body is a required field.")]
        public CarBody CarBody { get; set; }

        public Model Model { get; set; }

        [ForeignKey("Model")]
        public Guid ModelId { get; set; }

        [Required(ErrorMessage = "Drive Unit is a required field.")]
        public DriveUnit DriveUnit { get; set; }

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
