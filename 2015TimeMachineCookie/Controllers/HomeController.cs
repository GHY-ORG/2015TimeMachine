using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace _2015TimeMachineCookie.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {

            }
            //ViewBag.User = "Cookie";
            return View();
        }
    }
}