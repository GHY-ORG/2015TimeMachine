using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Entity;
using Common;
using BADL;
using _2015TimeMachineCookie.Models;

namespace _2015TimeMachineCookie.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 用户登陆接口，成功返回200 {username="..."}，否则401
        /// </summary>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("/User/Login")]
        public ActionResult Login(string username, string password)
        {
            password = (password != null ? Md5.MD5_encrypt(password) : "");
            En_User user = BADL_User.Login(username ?? "", password);
            if (user != null)
            {
                Session["User"] = user;
                return Json(new { username = user.UName });//200
            }
            else
            {
                return new HttpUnauthorizedResult("login err.");//401
            }
        }
        [HttpPost]
        public void Register(RegisterForm data)
        {
            ValidateModel(data);
            System.Diagnostics.Debug.WriteLine(data);
            Response.StatusCode = 200;
            Response.Write(JsonConvert.SerializeObject(new { username = data.UserName }));
        }
        /// <summary>
        /// 用户安全退出，返回Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/User/Exit")]
        public ActionResult Exit()
        {
            if (Session["User"] != null)
            {
                Session.Clear();
            }
            return Redirect("/Home/Index");
        }
    }
}