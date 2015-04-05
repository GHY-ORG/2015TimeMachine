using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using BADL;
using Entity;
using Common;
using System.Data;
using Newtonsoft.Json;

namespace _2015TimeMachineCookie.Controllers
{
    [RoutePrefix("/Picture")]
    public class PictureController : Controller
    {
        // GET: Picture
        public ActionResult Index()
        {
            DataTable dt = BADL_showImg.showImg(1, 1);
            return View(dt);
        }


        [HttpPost]
        public ActionResult Create(HttpPostedFileBase upImg)
        {
            if (Session["User"] == null) return Redirect("/Home/Index");
            var result = new List<int>();

            Parallel.For(0, Request.Files.Count, x =>
            {
                var savepath = string.Concat(Server.MapPath("~/UploadFiles/"), "", "");
                try
                {
                    Request.Files[x].SaveAs(savepath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                En_Img img = new En_Img();
            });

            return null;
        }
    }
}