using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Entity;

namespace BADL
{
    //显示图片,返回showImg的集合
    public class BADL_showImg
    {
        private static SqlHelper helper = new SqlHelper();
        static string constr_TM = System.Configuration.ConfigurationManager.ConnectionStrings["conTimeMachine"].ConnectionString;
        static string constr_U = System.Configuration.ConfigurationManager.ConnectionStrings["conGhyusers"].ConnectionString;

        //查询昵称和票数，返回imgTable
        public static DataTable reImgTable(DataTable imgTable)
        {
            string sql;
            foreach (DataRow dr in imgTable.Rows)
            {
                string uid = dr["uID"].ToString();
                sql = "select [uName] from [user] where uID=?";
                var param2 = new OleDbParameter("@uid", uid);
                object uName = helper.ExecuteScalar(constr_U, CommandType.Text, sql, param2);
                dr["uName"] = uName == null ? "" : uName.ToString();

                string imgid = dr["imgID"].ToString();
                sql = "select count(?) from [vote]";
                var param3 = new OleDbParameter("@imgid", imgid);
                object votes = helper.ExecuteScalar(constr_TM, CommandType.Text, sql, param3);
                dr["votes"] = votes == null ? 0 : Int32.Parse(votes.ToString());
            }
            return imgTable;
        }

        //显示某一类别的图片，参数：类别号
        public static DataTable showImg(int type, int blockno)
        {
            string sql;
            if (blockno == 1)
            {
                sql = @"select top 20 * from [img] where type = ? and sortOrder = 1 and state = 0";
            }
            else
            {
                sql = @"select top 20 * from [img] where type=? and sortOrder=1 and state=0 and [imgID] not in select top " + 20 * (blockno - 1) + " [imgID] from [img] where type=" + type + " and sortOrder=1 and state=0";
            }
            var param = new OleDbParameter("@type", type);
            DataTable imgTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);
            imgTable.Columns.Add("uName", typeof(string));
            imgTable.Columns.Add("votes", typeof(Int32));

            return reImgTable(imgTable);
        }

        //显示符合搜索条件的图片，参数：搜索条件（学号，作者昵称）,类别号
        public static DataTable showImg_search(string information, int type)
        {
            string sql = @"select uID,uName from [user] where concat(uNum,uName) like '%?%'";
            var param1 = new OleDbParameter("@info", information);
            DataTable imgTable_uID = helper.ExcuteDataTable(constr_U, CommandType.Text, sql, param1);

            sql = @"select * from [img] where uID=? and sortOrder=1 and type=? and state=0";
            var param2 = new OleDbParameter[] { 
                new OleDbParameter ("@uid",new Guid(imgTable_uID.Rows[0][0].ToString())),
                new OleDbParameter("@type",type)
            };
            DataTable imgTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param2);

            return reImgTable(imgTable);
        }

        //显示按照时间顺序的图片，参数：序列方式（0：升序 1：倒序）、类别号
        public static DataTable showImg_time(int order, int type)
        {
            string sql = "select * from [img] where type=? and sortOrder=1 order by upTime" + (order == 0 ? "" : " desc");
            var param = new OleDbParameter("@type", type);
            DataTable imgTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);

            return reImgTable(imgTable);
        }

        //显示按照浏览次数的图片，参数：序列方式（0：升序 1：倒序）、类别号
        public static DataTable showImg_view(int order, int type)
        {
            string sql = "select * from [img] where type=? and sortOrder=1 and state=0 order by views" + (order == 0 ? "" : " desc");
            var param = new OleDbParameter("@type", type);
            DataTable imgTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);

            return reImgTable(imgTable);
        }

        //显示按照投票数的图片，参数：序列方式（0：升序 1：倒序）、类别号,（不能显示没有被投票的）
        public static DataTable showImg_vote(int order, int type)
        {
            string sql = "select img.* from [img],[vote] where img.imgID=vote.imgID and type=? and sortOrder=1 and state=0 group by vote.imgID order by count(vote.imgID)" + (order == 0 ? "" : " desc");
            var param = new OleDbParameter("@type", type);
            DataTable imgTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);

            return reImgTable(imgTable);
        }
    }
}
