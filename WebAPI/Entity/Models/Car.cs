using Enums;

namespace Entity.Models
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public byte[] Image { get; set; }
        public Fuel Fuel { get; set; }
        public CarBody CarBody { get; set; }
        public DriveUnit DriveUnit { get; set; }
        public  Model Model { get; set; }
        public int ModelId { get; set; }
        public  Lot Lot { get; set; }
        public int LotId { get; set; }
    }
}
