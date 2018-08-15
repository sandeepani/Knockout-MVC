using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Product
    {
        [Required] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        [DisplayName("Name")]
        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}