using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OzonShop.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public bool IsStore { get; set; }

        public double Price { get; set; }

        public String Picture { get; set; }
    }
}