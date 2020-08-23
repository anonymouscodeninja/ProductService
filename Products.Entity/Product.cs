using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Entity
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("price")]
        public decimal Price { get; set; }

        [DisplayName("Delivery Price")]
        public decimal DeliveryPrice { get; set; }

    }
}
