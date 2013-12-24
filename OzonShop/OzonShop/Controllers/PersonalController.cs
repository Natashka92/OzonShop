using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzonShop.Filters;
using OzonShop.DataAccess;
using Shop.DataBase;
using OzonShop.Models;

namespace OzonShop.Controllers
{
    [Culture]
    [Authorize]
    public class PersonalController : Controller
    {
        //
        // GET: /Personal/
        public ActionResult PersonalPage()
        {
            int id = new UserRepository().Find(User.Identity.Name).UserId;
            var products = BasketRepository.Select(id);
            var baskets = new List<BasketModel>();
            double totalPrice = 0;
            if (products != null)
            {
                foreach (var item in products)
                {
                    double rate = CurrencyRepository.Rate(CurrencyRepository.ReturnId(item.ProductId));
                    if (item.Discount != 0)
                        totalPrice += item.Price * item.Discount * rate;
                    else
                        totalPrice += item.Price * rate;
                    baskets.Add(new BasketModel()
                    {
                        ProductId = item.ProductId,
                        QuantityProduct = 1,
                        Name = item.Name,
                        Price = item.Price,
                        Discount = item.Discount,
                        CurrencyId = CurrencyRepository.ReturnId(item.ProductId)
                    });
                }
            }
            OrderModel order = new OrderModel() { TotalPrice = totalPrice, Basket = baskets };
            return View(order);
        }
               
        //
        // GET:/Personal/AddToBasket
        [HttpGet, ActionName("AddToBasket")]
        public ActionResult AddToBasket(int id)
        {
            Basket basket = new Basket() {
                ProductId=id, 
                UserId = new UserRepository().Find(User.Identity.Name).UserId 
            };
            BasketRepository.Add(basket);
            return RedirectToAction("ViewProduct", "Search", new { productId = id });
        }

        //
        // GET: /Personal/DeleteFromBasket
        [HttpGet, ActionName("DeleteFromBasket")]
        public ActionResult DeleteFromBasket(int idProduct, String nameUser)
        {
            int idUser = new UserRepository().Find(nameUser).UserId;
            BasketRepository.Delete(idProduct, idUser);
            return RedirectToAction("PersonalPage");
        }

        // GET: /Personal/CreateOrder
        public ActionResult CreateOrder(double totalPrice)
        {
            if (totalPrice == 0)
            {
                return View("PersonalPage");
            }
            var adress = AdressRepository.FirstOrDefault(new UserRepository().Find(User.Identity.Name).UserId);
            var order = new Order() { 
                UserId = new UserRepository().Find(User.Identity.Name).UserId,
                TotalPrice = totalPrice,
                OrderData = DateTime.Now,
                CurrencyId = 1,
                Currency = CurrencyRepository.Find(1),
                AdressId = adress!= null ? adress.AdressId : -1
            };
            ViewBag.Adresses = AdressRepository.Select(new UserRepository().Find(User.Identity.Name).UserId).ToArray();
            ViewBag.Currency = CurrencyRepository.Select().ToArray();

            return View(order);
        }
        //
        // GET: /Personal/Order/
        public ActionResult Orders()
        {
            var order = new OrderRepository().Select(new UserRepository().Find(User.Identity.Name).UserId);
            return View(order);
        }

        // POST: /Personal/CreateOrder
        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.AdressId != -1 )
                {
                    int orderId = new OrderRepository().Add(order);
                    int id = new UserRepository().Find(User.Identity.Name).UserId;
                    var products = BasketRepository.Select(id);
                  
                    foreach (var item in products)
                    {
                        new OrderRepository().AddOrderProduct(new OrderProduct() { 
                            OrderId = orderId,
                            ProductId = item.ProductId,
                            Quantity = 1,
                        });
                        BasketRepository.Delete(item.ProductId, id);
                    }
                    return View("message");
                }
            }
            return View(order);
        }
        
        //
        // GET: /Adress
        public ActionResult Adress()
        {
            var adresses = AdressRepository.Select(User.Identity.Name);
            return View(adresses);
        }

        //
        // GET: /Personal/EditAdress/5
        public ActionResult EditAdress(int id=0)
        {
            Shop.DataBase.Adress adress = AdressRepository.Find(id);
            if (adress == null)
            {
                return HttpNotFound();
            }
            return View(adress);
        }

        //
        // POST: /Personal/EditAdress/5
        [HttpPost]
        public ActionResult EditAdress(Shop.DataBase.Adress adress)
        {
            if (ModelState.IsValid)
            {
                AdressRepository.Edit(adress);
                return RedirectToAction("Adress");
            }
            return View(adress);
        }

        //
        // GET: /Personal/CreateAdress
        public ActionResult CreateAdress()
        {
            return View();
        }

        //
        // POST: /Personal/CreateAdress
        [HttpPost]
        public ActionResult CreateAdress(Shop.DataBase.Adress adress)
        {
            adress.UserId = new UserRepository().Find(User.Identity.Name).UserId;
            if (ModelState.IsValid)
            {
                AdressRepository.Add(adress);
                return RedirectToAction("Adress", "Personal");
            }
            return View(adress);
        }

        //
        // GET: /Personal/DeleteAdress
        [HttpGet, ActionName("DeleteAdress")]
        public ActionResult DeleteAdress(int id)
        {
            AdressRepository.Delete(id);
            return RedirectToAction("Adress");
        }
    }
}
