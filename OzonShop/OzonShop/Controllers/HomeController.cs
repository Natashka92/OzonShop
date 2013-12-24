using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzonShop.Filters;
using Shop.DataBase;
using OzonShop.Models;
using OzonShop.DataAccess;

namespace OzonShop.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        //
        // GET: /Home/CreateComment
        public ActionResult CreateComment(String comments, int productId)
        {
            int userId = new UserRepository().Find(User.Identity.Name).UserId;
            DescriptionRepository.Add(comments, productId, userId);
            return RedirectToAction("ViewProduct", "Search", new { productId = productId });
        }

        //
        // GET: /Home/Index
        public ActionResult Index()
        {
            return View(new CategoryRepository().Select().Where(s => s.Parent == null).ToList());
        }

        //
        // GET: /Home/Category
        public ActionResult Category()
        {
            return PartialView(new CategoryRepository().Select().Where(s => s.Parent == null).ToList());
        }

        //
        // GET: /Home/MenuCategory
        public ActionResult MenuCategory(int parentId)
        {
            var catigories = new CategoryRepository().Select(parentId);
            return PartialView(catigories);
        }

        //
        // GET: /Home/Cloud
        public ActionResult Cloud()
        {
            var model = TagRepository.CreatorCloud(20).ToList();
            return PartialView(model);
        }

        //
        // GET: /Home/ChangeCulture
        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}
