using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace _2015TimeMachineCookie.Controllers
{
    public class UserController : Controller
    {
        // Post: UserLogin
        [HttpPost]
        [Route("/User/Login")]
        public ActionResult Login(string username, string password)
        {
            if ("cookie".Equals(username) && "123".Equals(password))
            {
                return Json(new { username = username, password = password });
            }
            else
            {
                return new HttpUnauthorizedResult("cookie err.");
            }
            
        }
    }
}