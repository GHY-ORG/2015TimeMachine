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

namespace _2015TimeMachineCookie.Controllers
{
    public class PictureController : Controller
    {
        // Get: Picture by BlockNo
        [HttpGet]
        public ActionResult Index(int type, int id)
        {
            DataTable dt = BADL_showImg.showImg(type, id);
            return View(dt);
        }

        // Get: Picture by Order
        [Route("/Picture/Index/")]
        public ActionResult Index(int select, int type, int blockno)
        {
            DataTable dt = BADL_showImg.showImg_time(select, type, blockno);
            return View(dt);
        }

        // Get: Picture by input
        [Route("/Picture/Index/")]
        public ActionResult Index(string input)
        {
            DataTable dt = BADL_showImg.showImg_search(input);
            return View(dt);
        }
        
    }
}