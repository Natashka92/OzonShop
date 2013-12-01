using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public virtual User User { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public int AdressId { get; set; }

        [Required]
        public DateTime OrderData { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        public virtual Currency Currency { get; set; }
        [Required]
        public int CurrencyId { get; set; }

        public virtual ICollection<OrderProduct> OrderProduct { get; set; }

        public static List<Order> Select()
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Orders.ToList();
            }
        }

        public static List<Order> Select(DateTime searchString)
        {
            using (var dbContext = new DataContext())
            {
                var orders = Select();
                return orders = orders.Where(s => s.OrderData.CompareTo(searchString) >= 0).ToList();
            }
        }

        public static void Delete(int id)
        {
            using (var dbContext = new DataContext())
            {
                Order order = dbContext.Orders.Find(id);
                dbContext.Orders.Remove(order);
                dbContext.SaveChanges();
            }
        }

        public static Order Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Orders.Find(id);
            }
        }

        public static List<OrderProduct> SelectOrderProduct(int orderId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.OrderProducts.Where(s => s.OrderId == orderId).ToList();
            }
        }
    }
}
