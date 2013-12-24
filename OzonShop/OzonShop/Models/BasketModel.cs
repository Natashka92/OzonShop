using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzonShop.Models
{
    public class BasketModel
    {
        public int ProductId { get; set; }
        public String Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int QuantityProduct { get; set; }
        public int CurrencyId { get; set; }
    }
}