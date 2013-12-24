using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzonShop.Models
{
    public class OrderModel
    {
        public double TotalPrice { get; set; }
        public List<BasketModel> Basket { get; set; }
    }
}