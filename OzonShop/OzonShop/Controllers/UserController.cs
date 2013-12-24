using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzonShop.Filters;
using Shop;
using Shop.DataBase;
using OzonShop.Models;
using OzonShop.DataAccess;

namespace OzonShop.Controllers
{
    [Culture]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IRepositoryUser repositoryUser;
        
        public UserController(IRepositoryUser repositoryUser)
        {
            this.repositoryUser = repositoryUser;
        }

        //
        // GET: /User/
        public ActionResult ViewUser(string SearchString)
        {
            var users = repositoryUser.Select();
            if (!String.IsNullOrEmpty(SearchString))
            {
                users = repositoryUser.Select(SearchString);
            }
            return View(users);
        }

        //
        // GET: /Admin/DetailsUser/5
        public ActionResult DetailsUser(int id)
        {
            User user = repositoryUser.Find(id);
            ViewBag.Adress = AdressRepository.Select(id);
            return View(user);
        }

        //
        // GET: /Admin/DeleteUser
        [HttpGet, ActionName("DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            repositoryUser.Delete(id);
            return RedirectToAction("ViewUser");
        }
    }
}
