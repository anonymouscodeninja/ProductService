using System;
using System.ComponentModel.DataAnnotations;

namespace Products.Dto
{
    public class ProductOptionDto : ProductOptionCreateDto
    {
        [Required]
        public int Id { get; set; }
    }
}
