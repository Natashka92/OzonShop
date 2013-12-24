using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataBase;

namespace OzonShop.DataAccess
{
    public interface IRepositoryOrder
    {
        List<Order> Select();
        List<Order> Select(DateTime searchString);
        void Delete(int id);
        Order Find(int id);
        List<OrderProduct> SelectOrderProduct(int orderId);
    }
}