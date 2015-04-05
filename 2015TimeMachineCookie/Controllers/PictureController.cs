using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using Common;
using Entity;
using BADL;

namespace _2015TimeMachineCookie.Controllers
{
    public class PictureController : Controller
    {
        // GET: Picture
        public ActionResult Index()
        {
            DataTable dt = BADL_showImg.showImg(1, 1);
            return View(dt);
        }

        // Post: BlockNo
        [HttpPost]
        [Route("/picture/block/{blockno}")]
        public ActionResult block(int blockno)
        {
            DataTable dt = BADL_showImg.showImg(1, blockno);
            return View(dt);
        }
    }
}