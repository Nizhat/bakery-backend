using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageSrc { get; set; }
    }

    public class PutProductModel
    {
        public int id { get; set; }
        public Product Product { get; set; }
    }
    public class ProductDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageSrc { get; set; }
    }
}