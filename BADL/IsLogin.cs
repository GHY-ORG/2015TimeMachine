using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BADL
{
    public class IsLogin
    {
        public static string isLogin(object session, HttpCookie cookies, string n)//如果有cookie或session的话，已是登录状态，直接跳转
        {
            if (session == null)//session过期
            {
                string result = BADL_User.isExsitCookies(cookies);
                if (!result.Equals("0"))//cookies有效,直接登录
                {
                    session = BADL_User.GetUserID(result);//将userID写入session
                    return responseN(n, n);
                }
                else//cookies无效，重新登录
                {
                    return responseN("2", n);
                }
            }
            else//session未过期，直接登录
            {
                return responseN(n, n);
            }
        }

        public static string responseN(string n, string x)
        {
            if (n.Equals("0"))
            {
                return "self.location='upImg.aspx'";
            }
            else if (n.Equals("1"))
            {
                return "self.location='showImg.aspx'";
            }
            else if (n.Equals("2"))
            {
                return "alert('请先登录');self.location='login.aspx?n=" + x + "'";
            }
            else
            {
                return "alert('参数错误')";
            }
        }
    }
}
