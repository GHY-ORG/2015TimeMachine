using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        /// <summary>
        /// 用户登录返回json{message:"info"}
        /// 版本:release 1.0
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Register(RegisterForm data)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return Json(new { message = "表单中有非法有非法值！" });
            }
            //学号验证
            if (Common.CheckNum.CheckUserNum(data.StudentNo, data.SPassword))
            {

                En_User user = new En_User();
                user.UNum = data.StudentNo;
                user.UTel = data.Phone;
                user.UName = data.UserName;
                user.UPwd = Md5.MD5_encrypt(data.Password1);
                if (BADL_User.IsStunumExsit(user.UNum))
                {
                    Response.StatusCode = 400;
                    return Json(new { message = "该学号已经注册过用户！" });
                }
                if (BADL_User.IsNameExsit(user.UNum))
                {
                    Response.StatusCode = 400;
                    return Json(new { message = "用户名已经存在" });
                }

                if (BADL_User.RegisterUser(user))
                {
                    user = BADL_User.Login(user.UName, user.UPwd);
                    Session["User"] = user;
                    Response.StatusCode = 200;
                    return Json(new { message = "注册成功" });
                }
                else
                {
                    Response.StatusCode = 500;
                    return Json(new { message = "服务器问题，请联系管理员" });
                }

            }
            else
            {
                Response.StatusCode = 401;
                return Json(new { message = "学号验证失败" });
            }
        }
        /// <summary>
        /// 用户安全退出，返回Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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