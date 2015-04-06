using _2015TimeMachineCookie.Models;
using Entity;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace _2015TimeMachineCookie.Controllers
{
    public class UploadController : Controller
    {


        // GET: Upload
        public ActionResult Index()
        {
            //if (Session["User"] == null)
            //    return Redirect("/Home/Index");


            //显示用户图片信息
            DataTable dt = new DataTable();
            return View(dt);
        }

        [HttpPost]
        public ActionResult AddImage(UploadPicForm input, HttpPostedFileBase file)
        {
            En_Img image = new En_Img();
            En_User user = Session["User"] as En_User;

            if (user == null) return Redirect("/Home/Index");
            if (!ModelState.IsValid || file == null)
            {
                Response.StatusCode = 400;
                return new EmptyResult();
            }

            string path = GetPath(user.UName, input.Type, input.Order);
            string filename = path + DateTime.Now.Ticks + ".png";
            try
            {
                using (var stream = file.InputStream)
                {
                    Image img = Image.FromStream(stream);
                    var bmp = ResizeImg(img);
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                    bmp.Save(filename, ImageFormat.Png);
                }
            }
            catch
            {
                Response.StatusCode = 400;
                return new EmptyResult();
            }
            image.ImgPath = filename;
            image.Title = input.Title;
            image.ImgName = file.FileName;
            image.ImgTime = input.Time;
            image.SortOrder = input.Order;
            image.Type = input.Type;
            image.Description = input.Description;
            image.ImgLocation = input.Address;
            image.UID = user.UID;
            if (!BADL.BADL_Img.AddImg(image))
            {
                Response.StatusCode = 400;
                return new EmptyResult();
            }

            return Redirect("/Upload/Index");
        }
        #region 私有成员
        private Bitmap ResizeImg(Image input)
        {
            if (input.Width > 1600 || input.Height > 1600)
            {
                if (input.Width > input.Height)
                {
                    return new Bitmap(input, 1600, input.Height * 1600 / input.Width);
                }
                else
                {
                    return new Bitmap(input, input.Width * 1600 / input.Height, 1600);
                }
            }
            return new Bitmap(input);
        }
        private string GetPath(string uname, int type, int order)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Server.MapPath("/UpLoadFiles/"));
            sb.Append(uname);
            sb.Append(@"\");
            sb.Append(type);
            sb.Append(@"\");
            sb.Append(order);
            sb.Append(@"\");
            return sb.ToString();
        }
        #endregion

    }
}