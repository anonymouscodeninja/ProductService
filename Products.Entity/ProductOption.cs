using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Entity
{
    [Table("ProductOptions")]
    public class ProductOption
    {
        [Key]
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("ProductId")]
        public int ProductId { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

    }
}
