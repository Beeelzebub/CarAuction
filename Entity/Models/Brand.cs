using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Brand name is required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for Brand Name is 50 characters.")]
        public string BrandName { get; set; }

        public List<Model> Models { get; set; } = new List<Model>();
    }
}
