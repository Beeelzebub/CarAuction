using System.Collections.Generic;

namespace Entity.Models
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public  ICollection<Model> Models { get; set; } 
        public  ICollection<Car> Cars { get; set; }
    }
}
