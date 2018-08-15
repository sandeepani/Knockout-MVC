using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        //public DateTime CreatedUser { get; set; }

        public List<SaleDetail> SaleDetails { get; set; }

        public decimal GrossAmount { get; set; }

        public string PaymentType { get; set; }
    }
}