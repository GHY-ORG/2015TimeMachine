using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using Common;
using Entity;

namespace BADL
{
    public class BADL_Img
    {
        private static SqlHelper helper = new SqlHelper();
        static string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conTimeMachine"].ConnectionString;

        public static bool isUpImg(Guid uid)//判断用户是否上传过照片
        {
            string sql = @"update [img] set uID=? where uID=? and type = 1";
            var param = new OleDbParameter[] { 
                new OleDbParameter("@uID1",uid),
                new OleDbParameter("@uID2",uid)
            };
            int count;
            count = helper.ExecuteNonQuery(constr, CommandType.Text, sql, param);
            if (count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isUpStory(Guid uid)//判断用户是否上传过图片故事
        {
            string sql = @"update [img] set uID=? where uID=? and type = 2";
            var param = new OleDbParameter[] { 
                new OleDbParameter("@uID1",uid),
                new OleDbParameter("@uID2",uid)
            };
            int count;
            count = helper.ExecuteNonQuery(constr, CommandType.Text, sql, param);
            if (count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool InsertImg(En_Img img)//上传照片
        {
            string sql = @"INSERT INTO [TimeMachine].[dbo].[img]([imgID],[uID],
                [imgName],[imgTime],[imgLocation],[imgPath],[type],[sortOrder],
                [title],[description],[state])
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            var param = new OleDbParameter[] {
                new OleDbParameter("@pImgID",img.ImgID),
                new OleDbParameter("@pUID",img.UID),
                new OleDbParameter("@pName",img.ImgName),
                new OleDbParameter("@pTime",img.ImgTime.ToString("yyyy-MM-dd HH:mm:ss")),
                new OleDbParameter("@pLocation",img.ImgLocation),
                new OleDbParameter("@pPath",img.ImgPath),
                new OleDbParameter("@pType",img.Type),
                new OleDbParameter("@pSortOrder",img.SortOrder),
                new OleDbParameter("@pTitle",img.Title),
                new OleDbParameter("@pDescription",img.Description),
                new OleDbParameter("@pState", img.State)
            };
            return helper.ExecuteNonQuery(constr, CommandType.Text, sql, param) > 0 ? true : false;
        }

        public static int IsBlankUp(En_Img eu)//注册字符串长度判断
        {
            if (eu.Title.Equals("") || eu.Title.Length > 50)
            {
                return 1;//题目长度不符
            }
            else if (eu.ImgLocation.Equals("") || eu.ImgLocation.Length > 50)
            {
                return 3;//拍摄地点长度不符
            }
            else if (eu.Description.Equals("") || eu.Description.Length > 100)
            {
                return 4;//照片描述长度不符
            }
            else
                return 5;
        }

        // <summary>
        /// 保存图片文件,返回保存后图片文件的命名。
        /// </summary>
        /// <param name="musicname"></param>
        /// <param name="musicdesribe"></param>
        /// <param name="file"></param>
        public static string SavePic(System.Web.HttpPostedFile file)    //将图片重命名
        {
            string[] fileNameBlock = file.FileName.Split(new Char[] { '.' });
            string lastname = fileNameBlock[fileNameBlock.Length - 1].ToLower();
            if (IsPicFile(file))//true
            {
                string savePath = System.AppDomain.CurrentDomain.BaseDirectory + @"UpLoadFiles/";
                //string fileName = file.FileName;
                //获取程序基目录Result: C:\xxx\xxx\
                string fileName = "Pic" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + "." + lastname;
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                string pathToCheck = savePath + fileName;
                string tempfileName = "";
                //检查文件是否重名
                if (System.IO.File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while
                    (System.IO.File.Exists(pathToCheck))
                    {
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = savePath + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;
                }
                string[] fileNameSplit = fileName.Split(new Char[] { '.' });
                string newsavePath = savePath + "pre" + fileNameSplit[0] + ".jpeg";
                savePath += fileName;
                file.SaveAs(savePath);
                BADL_Img.GetPicThumbnail(savePath, newsavePath, 70);
                System.IO.File.Delete(savePath);
                return "pre" + fileNameSplit[0] + ".jpeg";
            }
            else
            {
                return "false";
            }
        }
        public static string SavePic(System.Web.HttpPostedFile file, int count)
        {
            string[] fileNameBlock = file.FileName.Split(new Char[] { '.' });
            string lastname = fileNameBlock[fileNameBlock.Length - 1].ToLower();
            if (IsPicFile(file))//true
            {
                string savePath = System.AppDomain.CurrentDomain.BaseDirectory + @"UpLoadFiles/";

                string fileName = "Pic" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + "." + lastname;
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                string pathToCheck = savePath + fileName;
                string[] fileNameSplit = fileName.Split(new Char[] { '.' });
                string newsavePath = savePath + "pre" + fileNameSplit[0] + count + ".jpeg";
                savePath += fileName;
                file.SaveAs(savePath);
                BADL_Img.GetPicThumbnail(savePath, newsavePath, 70);
                System.IO.File.Delete(savePath);
                return "pre" + fileNameSplit[0] + count + ".jpeg";
            }
            else
            {
                return "false";
            }
        }
        /// <summary>
        /// 验证是否为图片文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsPicFile(System.Web.HttpPostedFile file)//判断是否是图片文件
        {
            string filename = file.FileName;
            string lastname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            if (lastname.Equals("jpg") || lastname.Equals("jpeg") || lastname.Equals("png") || lastname.Equals("bmp") || lastname.Equals("gif"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="sFile">文件的源地址</param>
        /// <param name="outPath">保存压缩图片文件的新地址</param>
        /// <param name="flag">压缩的百分比</param>
        /// <returns></returns>
        public static bool GetPicThumbnail(string sFile, string outPath, int flag)//压缩图片质量和大小
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);

            ImageFormat tFormat = iSource.RawFormat;
            //以下代码为保存图片时，设置压缩质量 
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100 
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                iSource = SizeDown(iSource);
                if (jpegICIinfo != null)
                {
                    iSource.Save(outPath, jpegICIinfo, ep);//dFile是压缩后的新路径 
                }
                else
                {
                    iSource.Save(outPath, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                iSource.Dispose();
            }
        }
        public static Image SizeDown(Image iSource)
        {
            int width = (int)(iSource.Width);
            int height = (int)(iSource.Height);
            float wh = ((float)width / (float)height);
            if (width >= height && width > 1500)
            {
                width = 1500;
                height = (int)(1500 / wh);
            }
            else if (height > width && height > 1125)
            {
                height = 1125;
                width = (int)(1125 * wh);
            }
            else
            {
            }
            Bitmap init = new Bitmap(iSource, new Size(width, height));
            iSource.Dispose();
            return (Bitmap)init;
        }
    }
}
