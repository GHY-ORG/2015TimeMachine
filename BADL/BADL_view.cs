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
    public class BADL_view
    {
        private static SqlHelper helper = new SqlHelper();
        static string constr_TM = System.Configuration.ConfigurationManager.ConnectionStrings["conTimeMachine"].ConnectionString;

        //浏览
        public static bool view(Guid imgID)
        {
            string sql = "update [TimeMachine].[dbo].[img] set views=views+1 where imgID=" + imgID + " and sortOrder=1";
            var param = new OleDbParameter();
            return helper.ExecuteNonQuery(constr_TM, CommandType.Text, sql, param) > 0 ? true : false;
        }
    }
}
