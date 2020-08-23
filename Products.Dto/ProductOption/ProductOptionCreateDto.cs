using System;
using System.ComponentModel.DataAnnotations;

namespace Products.Dto
{
    public class ProductOptionCreateDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
