using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Shop.DataBase;

namespace Shop.Test
{
    [TestFixture]
    class BaseDBTest
    {
        static protected string category1 = "Category1";
        static protected string category2 = "Category2";
        static protected string category3 = "Category3";
        static private string dataBase = "Shop";
        static private int num = 0;

        [Test]
        public static void AddCategory()
        {
            num++;
            using (var dbContext = new DataContext(dataBase+num.ToString()))
            {
                Category cat1 = new Category() { CategoryId = 1, Name = category1, Parent = null };
                Category cat2 = new Category() { CategoryId = 2, Name = category1, Parent = cat1 };
                Category cat3 = new Category() { CategoryId = 3, Name = category1, Parent = cat1 };
                dbContext.Categories.Add(cat1);
                dbContext.Categories.Add(cat2);
                dbContext.Categories.Add(cat3);
                dbContext.SaveChanges();
                Assert.AreEqual(3, dbContext.Categories.Count());
                
                dbContext.Categories.Remove(cat2);               
                dbContext.SaveChanges();
                Assert.AreEqual(2, dbContext.Categories.Count());

                dbContext.Categories.Remove(cat3);
                dbContext.SaveChanges();
                Assert.AreEqual(1, dbContext.Categories.Count());

                dbContext.Categories.Remove(cat1);
                dbContext.SaveChanges();
                Assert.AreEqual(0, dbContext.Categories.Count());
            }            
        }

        [Test]
        public static void AddCurrency()
        {
            num++;
            using (var dbContext = new DataContext(dataBase+num.ToString()))
            {
                string name = "RUR";
                if (dbContext.Currencies.FirstOrDefault(x => x.Name == name) == null)
                {
                    dbContext.Currencies.Add(new Currency() { Name = name, Rate = 1 });
                    dbContext.SaveChanges();
                }
                Assert.AreEqual(1, dbContext.Currencies.Count());
            }
        }
        
        [Test]
        public static void AddProduct()
        {
            num++;
            using (var dbContext = new DataContext(dataBase + num.ToString()))
            {
                Category cat4 = new Category() { CategoryId = 14, Name = "category14", Parent = null };
                dbContext.Categories.Add(cat4);               
                dbContext.SaveChanges();

                int productId = 123123;
                int categoryId = 14;
                string vendorName = "Sumsung";
                VendorName vendorN = dbContext.VendorNames.FirstOrDefault(x=>x.Name == vendorName);
                if (vendorN == null)
                {
                    VendorName vendor = new VendorName() { Name = vendorName };
                    dbContext.VendorNames.Add(vendor);
                }

                var vendorCode = "GT-5000";
                Vendor vendorV = dbContext.Vendors.FirstOrDefault(x=>x.VendorCode == vendorCode);
                if (vendorV == null)
                {
                    vendorV = new Vendor() { VendorCode = vendorCode, VendorName = vendorN };
                    dbContext.Vendors.Add(vendorV);
                }

                var listPicture = new List<Picture>();
                for(int i = 0; i<3; i++)
                {
                    Picture picture = new Picture { PictureUrl = "pic" + i, ProductId = productId };
                    dbContext.Pictures.Add(picture);
                    listPicture.Add(picture);
                }

                var product = new Product()
                {
                    ProductId = productId,
                    Barcode = "barcode",
                    CategoryId = categoryId,
                    Currency = dbContext.Currencies.Find(1),
                    Description = "description",
                    Discount = 0.0,
                    Name = "name",
                    Price = 123.6,
                    Url = "url\\",
                    Vendor = vendorV,
                    Picture = listPicture
                };
                dbContext.Products.Add(product);
                dbContext.SaveChanges();

                for (int i = 0; i < 4; i++ )
                {
                    var paramName = "name" + i;
                    ParamName paramN = dbContext.ParamNames.FirstOrDefault(x => x.Name == paramName);
                    if (paramN == null)
                    {
                        paramN = new ParamName() { Name = paramName };
                        dbContext.ParamNames.Add(paramN);
                        dbContext.SaveChanges();
                    }
                    var paramValue = "Value" + i;
                    ParamValue paramV = dbContext.ParamValues.FirstOrDefault(x => x.Name == paramValue);
                    if (paramV == null)
                    {
                        paramV= new ParamValue() { Name = paramValue };
                        dbContext.ParamValues.Add(paramV);
                        dbContext.SaveChanges();
                    }

                    dbContext.Parameters.Add(new Parameter()
                    {
                        ParamNameId = paramV.ParamValueId,
                        ParamValueId = paramN.ParamNameId,
                        ProductId = productId
                    });
                }
                dbContext.SaveChanges();

                Assert.AreEqual(1, dbContext.Products.Count());
                Assert.AreEqual(3, dbContext.Pictures.Count());
                Assert.AreEqual(4, dbContext.Parameters.Count());
                Assert.AreEqual(1, dbContext.Vendors.Count());
                Assert.AreEqual(1, dbContext.VendorNames.Count());
                Assert.AreEqual(1, dbContext.Categories.Count());

                dbContext.Products.Remove(product);
                dbContext.SaveChanges();

                Assert.AreEqual(0, dbContext.Products.Count());
                Assert.AreEqual(0, dbContext.Pictures.Count());
                Assert.AreEqual(0, dbContext.Parameters.Count());
                Assert.AreEqual(1, dbContext.Categories.Count());

                dbContext.Categories.Remove(cat4);
                dbContext.SaveChanges();
                Assert.AreEqual(0, dbContext.Categories.Count());
            }
        }
        
        [Test]
        public static void AddUser()
        {
            num++;
            using (var dbContext = new DataContext(dataBase+num.ToString()))
            {
                Adress adress1 = new Adress() { Country = "12", Street = "12", Town = "12" };
                Adress adress2 = new Adress() { Country = "13", Street = "13", Town = "13" };

                dbContext.Adresses.Add(adress1);
                dbContext.Adresses.Add(adress2);

                List<Adress> adresses = new List<Adress>();
                adresses.Add(adress1);
                adresses.Add(adress2);

                User user = new User() { UserName = "Natali", Email = "mail.ru", Phone = "12-12-12", Login = "123", Adress = adresses };
                dbContext.Users.Add(user);

                dbContext.SaveChanges();

                Assert.AreEqual(1, dbContext.Users.Count());
                Assert.AreEqual(2, dbContext.Adresses.Count());

                dbContext.Users.Remove(user);
                dbContext.SaveChanges();

                Assert.AreEqual(0, dbContext.Users.Count());
                Assert.AreEqual(0, dbContext.Adresses.Count());
            }
        }
        
        [Test]
        public static void AddOrder()
        {
            num++;
            using (var dbContext = new DataContext(dataBase+num.ToString()))
            {
                Adress adress1 = new Adress() { Country = "12", Street = "12", Town = "12" };
                Adress adress2 = new Adress() { Country = "13", Street = "13", Town = "13" };

                dbContext.Adresses.Add(adress1);
                dbContext.Adresses.Add(adress2);

                List<Adress> adresses = new List<Adress>();
                adresses.Add(adress1);
                adresses.Add(adress2);

                User user = new User() { UserName = "Natali", Email = "mail.ru", Phone = "12-12-12", Login = "123", Adress = adresses };
                dbContext.Users.Add(user);                                                            

                Category cat5 = new Category() { CategoryId = 25, Name = "category25", Parent = null };
                dbContext.Categories.Add(cat5);               

                dbContext.Currencies.Add(new Currency() { Name = "RUR", Rate = 1 });
                dbContext.SaveChanges();

                int productId = 1231234;
                int categoryId = 25;
                string vendorName = "Sumsung";
                VendorName vendorN = dbContext.VendorNames.FirstOrDefault(x => x.Name == vendorName);
                if (vendorN == null)
                {
                    VendorName vendor = new VendorName() { Name = vendorName };
                    dbContext.VendorNames.Add(vendor);
                }

                var vendorCode = "GT-5000";
                Vendor vendorV = dbContext.Vendors.FirstOrDefault(x => x.VendorCode == vendorCode);
                if (vendorV == null)
                {
                    vendorV = new Vendor() { VendorCode = vendorCode, VendorName = vendorN };
                    dbContext.Vendors.Add(vendorV);
                }

                var listPicture = new List<Picture>();
                for (int i = 0; i < 3; i++)
                {
                    Picture picture = new Picture { PictureUrl = "pic" + i, ProductId = productId };
                    dbContext.Pictures.Add(picture);
                    listPicture.Add(picture);
                }

                var product = new Product()
                {
                    ProductId = productId,
                    Barcode = "barcode",
                    CategoryId = categoryId,
                    Currency = dbContext.Currencies.Find(1),
                    Description = "description",
                    Discount = 0.0,
                    Name = "name",
                    Price = 123.6,
                    Url = "url\\",
                    Vendor = vendorV,
                    Picture = listPicture
                };
                dbContext.Products.Add(product);
                dbContext.SaveChanges();

                Basket basket1 = new Basket() { ProductId = 1231234, UserId = 1 };
                dbContext.Baskets.Add(basket1);
                Basket basket2 = new Basket() { ProductId = 1231234, UserId = 1 };
                dbContext.Baskets.Add(basket2);
                dbContext.SaveChanges();

                Assert.AreEqual(2, dbContext.Baskets.Count());
                Order order = new Order() { AdressId = 1, CurrencyId = 1, OrderData = DateTime.Now, UserId = 1, Currency = dbContext.Currencies.Find(1), TotalPrice = 2345 };
                dbContext.Orders.Add(order);

                int k = dbContext.Baskets.Where(m => m.UserId == 1).Count();
                for (int i = 0; i <k ; i++)
                {
                    Basket basket = dbContext.Baskets.First(x => x.UserId == 1);                    
                    OrderProduct orderProduct = new OrderProduct() { OrderId = 1, ProductId = basket.ProductId, Quantity = 2 };
                    dbContext.OrderProducts.Add(orderProduct);
                    dbContext.Baskets.Remove(basket);
                    dbContext.SaveChanges();
                }
                dbContext.SaveChanges();

                Assert.AreEqual(1, dbContext.Users.Count());
                Assert.AreEqual(2, dbContext.Adresses.Count());
                Assert.AreEqual(1, dbContext.Products.Count());
                Assert.AreEqual(1, dbContext.Categories.Count());
                Assert.AreEqual(1, dbContext.Currencies.Count());
                Assert.AreEqual(1, dbContext.Orders.Count());
                Assert.AreEqual(2, dbContext.OrderProducts.Count());
                Assert.AreEqual(0, dbContext.Baskets.Count());

                dbContext.Categories.Remove(cat5);
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();

                Assert.AreEqual(0, dbContext.Products.Count());
                Assert.AreEqual(0, dbContext.Categories.Count());
                Assert.AreEqual(1, dbContext.Orders.Count());
                Assert.AreEqual(0, dbContext.OrderProducts.Count());
                Assert.AreEqual(1, dbContext.Users.Count());
                Assert.AreEqual(2, dbContext.Adresses.Count());

                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                Assert.AreEqual(0, dbContext.Users.Count());
                Assert.AreEqual(0, dbContext.Adresses.Count());
                Assert.AreEqual(0, dbContext.Orders.Count());
                Assert.AreEqual(0, dbContext.OrderProducts.Count());
            }
        }         
    }
}
