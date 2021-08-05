using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Model
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Brand name is required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string Name { get; set; }

        [Required]
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        public Guid BrandId { get; set; }
    }
}