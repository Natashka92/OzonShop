using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzonShop.Filters;

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
            return View();
        }

    }
}
