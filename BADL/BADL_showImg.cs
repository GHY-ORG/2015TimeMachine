using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;
using Common;
using Entity;

namespace BADL
{
    public class BADL_showImg
    {
        private static SqlHelper helper = new SqlHelper();
        static string constr_TM = System.Configuration.ConfigurationManager.ConnectionStrings["conTimeMachine"].ConnectionString;
        static string constr_U = System.Configuration.ConfigurationManager.ConnectionStrings["conGhyusers"].ConnectionString;

        //乱序显示某一类别的图片，参数：类别号
        public static List<En_showImg> showImg_outOfOrder(int type)
        {
            string sql = "select imgID,imgPath,views,uID from img where type=" + type + " and sortOrder=1";
            var param = new OleDbParameter();
            DataTable imgTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);

            List<En_showImg> showImgTable = new List<En_showImg>();

            for (int i = 0; i < imgTable.Rows.Count; i++)
            {
                En_showImg showImgRow = new En_showImg();
                showImgRow.ImgID = (Guid)imgTable.Rows[i][0];
                showImgRow.ImgPath = (string)imgTable.Rows[i][1];
                showImgRow.Views = (int)imgTable.Rows[i][2];

                sql = "select uName from user where uID=@uid";
                var newparam = new OleDbParameter[] { 
                    new OleDbParameter ("@uid",new Guid(imgTable.Rows[i][3].ToString()))
                };
                DataTable imgTable_UName = helper.ExcuteDataTable(constr_U, CommandType.Text, sql, newparam);
                showImgRow.UName = (string)imgTable_UName.Rows[0][0];

                sql = "select count(*) from vote where imgID=@imgid";
                newparam = new OleDbParameter[] { 
                    new OleDbParameter ("@imgid",showImgRow.ImgID)
                };
                DataTable imgTable_votes = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, newparam);
                showImgRow.Votes = (int)imgTable_votes.Rows[0][0];

                showImgTable.Add(showImgRow);
            }

            Random random = new Random();
            List<En_showImg> newList = new List<En_showImg>();
            foreach (En_showImg item in showImgTable)
            {
                newList.Insert(random.Next(newList.Count), item);
            }
            return newList;
        }

        //显示符合搜索条件的图片，参数：搜索条件（学号，作者昵称）
        public static List<En_showImg> showImg_search(string information, int type)
        {
            string sql = "select uID,uName from user where concat(uNum,uName) like %" + information + "%";
            var param = new OleDbParameter();
            DataTable imgTable_uID = helper.ExcuteDataTable(constr_U, CommandType.Text, sql, param);

            List<En_showImg> showImgTable = new List<En_showImg>();

            sql = "select imgID,imgPath,views from img where uID=" + imgTable_uID.Rows[0][0].ToString() + " and sortOrder=1 and type=" + type;
            DataTable imgTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);

            for (int i = 0; i < imgTable.Rows.Count; i++)
            {
                En_showImg showImgRow = new En_showImg();
                showImgRow.ImgID = (Guid)imgTable.Rows[i][0];
                showImgRow.ImgPath = (string)imgTable.Rows[i][1];
                showImgRow.Views = (int)imgTable.Rows[i][2];
                showImgRow.UName = (string)imgTable_uID.Rows[0][1];

                sql = "select count(*) from vote where imgID=@imgid";
                var newparam = new OleDbParameter[] { 
                    new OleDbParameter ("@imgid",showImgRow.ImgID)
                };
                DataTable imgTable_votes = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, newparam);
                showImgRow.Votes = (int)imgTable_votes.Rows[0][0];

                showImgTable.Add(showImgRow);
            }

            return showImgTable;
        }

        //显示按照时间顺序的图片，参数：序列方式（0：升序 1：倒序）、类别号
        public static List<En_showImg> showImg_time(int order, int type)
        {
            string sql = "select imgID,imgPath,views,uID from img where type=" + type + " and sortOrder=1 order by imgTime" + (order == 0 ? "" : " desc");
            var param = new OleDbParameter();
            DataTable imgTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);

            List<En_showImg> showImgTable = new List<En_showImg>();

            for (int i = 0; i < imgTable.Rows.Count; i++)
            {
                En_showImg showImgRow = new En_showImg();
                showImgRow.ImgID = (Guid)imgTable.Rows[i][0];
                showImgRow.ImgPath = (string)imgTable.Rows[i][1];
                showImgRow.Views = (int)imgTable.Rows[i][2];

                sql = "select uName from user where uID=@uid";
                var newparam = new OleDbParameter[] { 
                    new OleDbParameter ("@uid",new Guid(imgTable.Rows[i][3].ToString()))
                };
                DataTable imgTable_UName = helper.ExcuteDataTable(constr_U, CommandType.Text, sql, newparam);
                showImgRow.UName = (string)imgTable_UName.Rows[0][0];

                sql = "select count(*) from vote where imgID=@imgid";
                newparam = new OleDbParameter[] { 
                    new OleDbParameter ("@imgid",showImgRow.ImgID)
                };
                DataTable imgTable_votes = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, newparam);
                showImgRow.Votes = (int)imgTable_votes.Rows[0][0];

                showImgTable.Add(showImgRow);
            }

            return showImgTable;
        }

        //显示按照浏览次数的图片，参数：序列方式（0：升序 1：倒序）、类别号
        public static List<En_showImg> showImg_view(int order, int type)
        {
            string sql = "select imgID,imgPath,views,uID from img where type=" + type + " and sortOrder=1 order by views" + (order == 0 ? "" : " desc");
            var param = new OleDbParameter();
            DataTable imgTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);

            List<En_showImg> showImgTable = new List<En_showImg>();

            for (int i = 0; i < imgTable.Rows.Count; i++)
            {
                En_showImg showImgRow = new En_showImg();
                showImgRow.ImgID = (Guid)imgTable.Rows[i][0];
                showImgRow.ImgPath = (string)imgTable.Rows[i][1];
                showImgRow.Views = (int)imgTable.Rows[i][2];

                sql = "select uName from user where uID=@uid";
                var newparam = new OleDbParameter[] { 
                    new OleDbParameter ("@uid",new Guid(imgTable.Rows[i][3].ToString()))
                };
                DataTable imgTable_UName = helper.ExcuteDataTable(constr_U, CommandType.Text, sql, newparam);
                showImgRow.UName = (string)imgTable_UName.Rows[0][0];

                sql = "select count(*) from vote where imgID=@imgid";
                newparam = new OleDbParameter[] { 
                    new OleDbParameter ("@imgid",showImgRow.ImgID)
                };
                DataTable imgTable_votes = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, newparam);
                showImgRow.Votes = (int)imgTable_votes.Rows[0][0];

                showImgTable.Add(showImgRow);
            }

            return showImgTable;
        }

        //显示按照投票数的图片，参数：序列方式（0：升序 1：倒序）、类别号,（不能显示没有被投票的）
        public static List<En_showImg> showImg_vote(int order, int type)
        {
            string sql = "select vote.imgID,count(vote.imgID) from img,vote where img.imgID=vote.imgID and type=" + type + " and sortOrder=1 group by vote.imgID order by count(vote.imgID)" + (order == 0 ? "" : " desc");
            var param = new OleDbParameter();
            DataTable imgTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);

            List<En_showImg> showImgTable = new List<En_showImg>();

            for (int i = 0; i < imgTable.Rows.Count; i++)
            {
                En_showImg showImgRow = new En_showImg();
                showImgRow.ImgID = (Guid)imgTable.Rows[i][0];
                showImgRow.Votes = (int)imgTable.Rows[i][1];

                sql = "select imgPath,views,uID from img where imgID=@imgid";
                var newparam = new OleDbParameter[] { 
                    new OleDbParameter ("@imgid",showImgRow.ImgID)
                };
                DataTable imgTable_I = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);

                showImgRow.ImgPath = (string)imgTable_I.Rows[0][0];
                showImgRow.Views = (int)imgTable_I.Rows[0][1];

                sql = "select uName from user where uID=@uid";
                newparam = new OleDbParameter[] { 
                    new OleDbParameter ("@uid",new Guid(imgTable_I.Rows[0][2].ToString()))
                };
                DataTable imgTable_UName = helper.ExcuteDataTable(constr_U, CommandType.Text, sql, newparam);
                showImgRow.UName = (string)imgTable_UName.Rows[0][0];

                showImgTable.Add(showImgRow);
            }

            return showImgTable;
        }
    }
}
