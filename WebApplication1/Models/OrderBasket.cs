using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class OrderBasket
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        // Foreign Key
        public int OrderId { get; set; }
        // Navigation property
        public Order Order { get; set; }
    }
}