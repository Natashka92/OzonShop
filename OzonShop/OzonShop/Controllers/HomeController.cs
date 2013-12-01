using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzonShop.Filters;
using Shop.DataBase;
using OzonShop.Models;

namespace OzonShop.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Shop.DataBase.Category.Select().Where(s => s.Parent == null).ToList());
        }

        public ActionResult Category()
        {
            return PartialView(Shop.DataBase.Category.Select().Where(s => s.Parent == null).ToList());
        }

        public ActionResult MenuCategory(int parentId)
        {
            var catigories = Shop.DataBase.Category.Select(parentId);
            return PartialView(catigories);
        }

        public ActionResult Cloud()
        {
            var model = Tag.CreatorCloud(20).ToList();
            return PartialView(model);
        }

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
