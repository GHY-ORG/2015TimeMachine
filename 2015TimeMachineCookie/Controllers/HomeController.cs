using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Entity;
namespace _2015TimeMachineCookie.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 返回主页面，如果已经登陆将不会弹出登陆框
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            En_User user = Session["User"] as En_User;
            if (user != null)
            {
                ViewBag.User = user.UName;
            }
            return View();
        }



    }
}