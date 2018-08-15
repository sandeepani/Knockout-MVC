using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MvcApplication1.Models
{
    public class SaleDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SaleId { get; set; }

        public int ProductId { get; set; }

        //public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        
        public decimal Cost { get; set; }
    }
}
