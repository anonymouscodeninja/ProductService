using System;
using System.ComponentModel.DataAnnotations;

namespace Products.Dto
{
    public class ProductDto : ProductCreateDto
    {
        [Required]
        public int Id { get; set; }
    }
}
