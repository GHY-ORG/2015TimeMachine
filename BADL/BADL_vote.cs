using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;
using Common;

namespace BADL
{
    public class BADL_vote
    {
        private static SqlHelper helper = new SqlHelper();
        static string constr_TM = System.Configuration.ConfigurationManager.ConnectionStrings["conTimeMachine"].ConnectionString;
        static DateTime now;
        static DataTable voteTable;

        //投票,参数：投票者的uID，投票图片的imgID
        public static bool vote(Guid uID, Guid imgID)
        {
            if (vote_number(uID, imgID) && vote_repeat(uID, imgID))
            {
                string sql = "INSERT INTO [TimeMachine].[dbo].[vote](uID,imgID,time,state) VALUES (@uid, @imgid, @time, @state)";
                var param = new OleDbParameter[] {
                    new OleDbParameter("@uid",uID),
                    new OleDbParameter("@imgid",imgID),
                    new OleDbParameter("@time",now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new OleDbParameter("@state", 0)
                };
                return helper.ExecuteNonQuery(constr_TM, CommandType.Text, sql, param) > 0 ? true : false;
            }
            return false;
        }

        //检查投票次数是否超过限制,参数：投票者的uID，投票图片的imgID
        public static bool vote_number(Guid uID, Guid imgID)
        {
            now = System.DateTime.Now;

            string sql = "select imgID from vote where uID=@uid and DATEDIFF(day,time," + now + ")=0";
            var param = new OleDbParameter[] { 
                new OleDbParameter ("@uid",uID)
            };
            voteTable = helper.ExcuteDataTable(constr_TM, CommandType.Text, sql, param);

            if (voteTable.Rows.Count == 3)//今天已经投了三票
            {
                return false;
            }
            return true;
        }

        //检查是否是重复投票,参数：投票者的uID，投票图片的imgID
        public static bool vote_repeat(Guid uID, Guid imgID)
        {
            if (voteTable.Rows.Count == 0)//今天还没投过票
            {
                return true;
            }

            for (int i = 0; i < voteTable.Rows.Count; i++)
            {
                if (voteTable.Rows[i][0].Equals(imgID))
                    return false;
            }

            return true;
        }
    }
}
