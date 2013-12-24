using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataBase;
using OzonShop.Filters;
using OzonShop.DataAccess;

namespace OzonShop.Controllers
{
    [Culture]    
    public class OrderController : Controller
    {
        private readonly IRepositoryOrder RepositoryOrder;
        
        public OrderController(IRepositoryOrder RepositoryOrder)
        {
            this.RepositoryOrder= RepositoryOrder;
        }

        //
        // GET: /Order/
        [Authorize(Roles = "admin")]
        public ActionResult Order()
        {
            var order = RepositoryOrder.Select();
            return View(order);
        }

        //
        // GET: /Order/DetailsOrder/5
        public ActionResult DetailsOrder(int id)
        {
            Order order = RepositoryOrder.Find(id);
            var adress = AdressRepository.Find(order.AdressId);
            ViewBag.Adress = adress.Country + " " + adress.Town + " " + adress.Street;
            ViewBag.Currency = CurrencyRepository.Find(order.CurrencyId).Name;
            ViewBag.User = new UserRepository().Find(order.UserId).UserName;
            ViewBag.Products = RepositoryOrder.SelectOrderProduct(id);
            return View(order);
        }

        //
        // GET: /Order/DeleteOrder
        [Authorize(Roles = "admin")]
        [HttpGet, ActionName("DeleteOrder")]
        public ActionResult DeleteOrder(int id)
        {
            RepositoryOrder.Delete(id);
            return RedirectToAction("Order");
        }
    }
}
