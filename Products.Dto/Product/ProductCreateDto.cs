using System;
using System.ComponentModel.DataAnnotations;

namespace Products.Dto
{
    public class ProductCreateDto
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        public Decimal Price { get; set; }
        [Required]
        public Decimal DeliveryPrice { get; set; }

    }
}
