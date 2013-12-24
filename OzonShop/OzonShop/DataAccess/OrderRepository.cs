using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop;
using Shop.DataBase;
using System.Data.Entity;

namespace OzonShop.DataAccess
{
    public class OrderRepository : IRepositoryOrder
    {
        public List<Order> Select()
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Orders.ToList();
            }
        }

        public List<Order> Select(DateTime searchString)
        {
            using (var dbContext = new DataContext())
            {
                var orders = Select();
                return orders = orders.Where(s => s.OrderData.CompareTo(searchString) >= 0).ToList();
            }
        }

        public List<Order> Select(int userId)
        {
            using (var dbContext = new DataContext())
            {
                var orders = Select();
                return orders = orders.Where(s => s.UserId == userId).ToList();
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new DataContext())
            {
                Order order = dbContext.Orders.Find(id);
                dbContext.Orders.Remove(order);
                dbContext.SaveChanges();
            }
        }

        public Order Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Orders.Find(id);
            }
        }

        public List<OrderProduct> SelectOrderProduct(int orderId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.OrderProducts.Where(s => s.OrderId == orderId).ToList();
            }
        }

        public int AddOrderProduct(OrderProduct orderProduct)
        {
            using (var dbContext = new DataContext())
            {
                int id = dbContext.OrderProducts.Add(orderProduct).Id;
                var product = dbContext.Store.Find(orderProduct.ProductId);
                product.Quantity--;
                dbContext.Entry(product).State = EntityState.Modified;
                dbContext.SaveChanges();
                return id;
            }
        }

        public int Add(Order order)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                int id = dbContext.Orders
                    .Where(s => s.UserId == order.UserId)
                    .Where(s => s.TotalPrice == order.TotalPrice)
                    .FirstOrDefault(s => s.AdressId == order.AdressId).OrderId;
                return id;
            }
        }
    }
}