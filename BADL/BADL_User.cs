using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Sql;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Web;

using Entity;
using Common;

namespace BADL
{
    public class BADL_User
    {
        private static SqlHelper helper = new SqlHelper();
        static string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conGhyusers"].ConnectionString;
        /// <summary>
        /// 用户注册，user中要包含学号，手机，用户名，用户密码(md5)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool RegisterUser(En_User user)//注册
        {
            string sql = @"INSERT INTO [ghyusers].[dbo].[user]([uID], [uNum], [uTel], [uName],
                [uPwd], [uMail], [registerTime], [lastLogin], [state])
                VALUES (newid(), ?, ?, ?, ?, '', SYSDATETIME(), SYSDATETIME(), 1)";
            var param = new OleDbParameter[]{
                new OleDbParameter("@num",user.UNum),
                new OleDbParameter("@tel",user.UTel),
                new OleDbParameter("@name",user.UName),
                new OleDbParameter("@pwd",user.UPwd)
            };
            return helper.ExecuteNonQuery(constr, CommandType.Text, sql, param) > 0 ? true : false;
        }
        public static bool InsertUser(En_User user)
        {
            string sql = @"INSERT INTO [ghyusers].[dbo].[user]([uID],[uNum],[uName],
                [uPwd],[uMail],[uIP],[registerTime],[lastLogin],[state])
                VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            var param = new OleDbParameter[] { 
                new OleDbParameter("@pID",user.UID),
                new OleDbParameter("@pNum",user.UNum),
                new OleDbParameter("@pName",user.UName),
                new OleDbParameter("@pPwd",user.UPwd),
                new OleDbParameter("@pMail",user.UMail),
                new OleDbParameter("@pUIP",user.UIP),
                new OleDbParameter("@pRegistertime",user.RegisterTime.ToString("yyyy-MM-dd HH:mm:ss")),
                new OleDbParameter("@pLastLogin",user.LastLogin.ToString("yyyy-MM-dd HH:mm:ss")),
                new OleDbParameter("@pState", user.State)
            };
            return helper.ExecuteNonQuery(constr, CommandType.Text, sql, param) > 0 ? true : false;
        }

        public static En_User Login(string userInfo, string password)//登录判断，若成功，返回En_User对象
        {
            string regMail = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";
            string sql = "";


            if (System.Text.RegularExpressions.Regex.IsMatch(userInfo, regMail))//如果输入的是邮箱
            {
                sql = "select * from [user] where uMail=? and uPwd=?";
            }
            else//如果输入的是昵称
            {
                sql = "select * from [user] where uName=? and uPwd=?";
            }
            OleDbParameter[] param = new OleDbParameter[] {
                new OleDbParameter("@pUserInfo",userInfo),
                new OleDbParameter("@pPwd",password)
            };

            DataTable userTable = helper.ExcuteDataTable(constr, CommandType.Text, sql, param);

            if (userTable.Rows.Count == 1)
            {
                En_User eu = new En_User();
                object[] item = userTable.Rows[0].ItemArray;
                eu.UID = new Guid(item[0].ToString());
                eu.UNum = item[1].ToString();
                eu.UName = item[2].ToString();
                eu.UPwd = item[3].ToString();
                eu.UMail = item[4].ToString();
                eu.UGrade = item[5] == System.DBNull.Value ? 0 : Int32.Parse(item[5].ToString());
                eu.USex = item[6] == System.DBNull.Value ? 2 : Int32.Parse(item[6].ToString());
                eu.UTel = item[7].ToString();
                eu.UPic = item[8].ToString();
                eu.UIP = item[9].ToString();
                eu.RegisterTime = (DateTime)item[10];
                eu.TrueName = item[11].ToString();
                eu.State = Int32.Parse(item[12].ToString());
                eu.LastLogin = (DateTime)item[13];
                return eu;
            }
            else
                return null;
        }
        public static bool updatePwd(int worknum, string newPwd)//由学号修改密码
        {
            string sql = "update [user] set uPwd=? where uNum=?";
            var param = new OleDbParameter[] { 
                new OleDbParameter("@pPwd",newPwd),
                new OleDbParameter("@pNum",worknum)
            };
            return helper.ExecuteNonQuery(constr, CommandType.Text, sql, param) > 0 ? true : false;
        }
        public static En_User Login(string worknum)//验证学号是否已注册，若已注册，返回En_User对象
        {
            string sql = "select * from [user] where uNum=?";
            var param = new OleDbParameter("@pNum", worknum);
            DataTable userTable = helper.ExcuteDataTable(constr, CommandType.Text, sql, param);
            if (userTable.Rows.Count == 1)
            {
                En_User eu = new En_User();
                object[] item = userTable.Rows[0].ItemArray;
                eu.UID = new Guid(item[0].ToString());
                eu.UNum = item[1].ToString();
                eu.UName = item[2].ToString();
                eu.UPwd = item[3].ToString();
                eu.UMail = item[4].ToString();
                eu.UGrade = item[5] == System.DBNull.Value ? 0 : Int32.Parse(item[5].ToString());
                eu.USex = item[6] == System.DBNull.Value ? 2 : Int32.Parse(item[6].ToString());
                eu.UTel = item[7].ToString();
                eu.UPic = item[8].ToString();
                eu.UIP = item[9].ToString();
                eu.RegisterTime = (DateTime)item[10];
                eu.TrueName = item[11].ToString();
                eu.State = Int32.Parse(item[12].ToString());
                eu.LastLogin = (DateTime)item[13];
                return eu;
            }
            else
                return null;
        }
        public static int IsBlankReg(En_User eu)//注册字符串长度判断
        {
            if (eu.UNum.Equals("") || eu.UNum.Length > 20)
            {
                return 1;//学号长度不符
            }
            else if (eu.UName.Equals("") || eu.UName.Length > 20)
            {
                return 2;//昵称长度不符
            }
            else if (eu.UPwd.Equals("") || eu.UPwd.Length > 50)
            {
                return 3;//密码长度不符
            }
            else if (eu.UMail.Equals("") || eu.UMail.Length > 50)
            {
                return 4;//邮箱长度不符
            }
            else
                return 5;
        }

        public static int IsBlankAdd(En_User eu)//完善信息字符串判断
        {
            if (String.IsNullOrEmpty(eu.UTel) || eu.UTel.Length > 50)
            {
                return 1;//电话长度不符
            }
            else if (String.IsNullOrEmpty(eu.TrueName) || eu.TrueName.Length > 20)
            {
                return 2;//trueName长度不符
            }
            else
                return 3;
        }


        public static bool IsStunumExsit(string worknum)//学号是否存在
        {
            string update = "update [user] set uNum=? where uNum=?";
            var param = new OleDbParameter[] { 
                new OleDbParameter("@pNum",worknum),
                new OleDbParameter("@pNum2",worknum)
            };
            int count;
            count = helper.ExecuteNonQuery(constr, CommandType.Text, update, param);
            if (count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsNameExsit(string name)//昵称是否存在
        {
            string update = "update [user] set uName=?   where uName=?";
            var param = new OleDbParameter[] { 
                new OleDbParameter("@uName",name),
                new OleDbParameter("@uName",name)
            };
            int count;
            count = helper.ExecuteNonQuery(constr, CommandType.Text, update, param);
            if (count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsMailExsit(string umail)//邮箱是否存在
        {
            string update = "update [user] set uMail=?   where uMail=?";
            var param = new OleDbParameter[] { 
                new OleDbParameter("@uMail",umail),
                new OleDbParameter("@uMail",umail)
            };
            int count;
            count = helper.ExecuteNonQuery(constr, CommandType.Text, update, param);
            if (count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsTelExsit(string utel)//手机号是否存在
        {
            string update = "update [user] set uTel=?   where uTel=?";
            var param = new OleDbParameter[] { 
                new OleDbParameter("@uMail",utel),
                new OleDbParameter("@uMail",utel)
            };
            int count;
            count = helper.ExecuteNonQuery(constr, CommandType.Text, update, param);
            if (count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string isExsitCookies(HttpCookie cookies)//判断是否有cookies
        {
            if (cookies != null)//session过期，查看cookie是否存在
            {
                string[] message = cookies.Value.Split('+');
                if (message.Length == 2)
                {
                    string worknum = message[0];
                    string password = message[1];
                    if (BADL_User.CheckCookies(worknum, password))//判断cookie的真实性
                    {
                        return BADL_User.GetUserID(worknum);
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        public static bool CheckCookies(string worknum, string password)//验证cookies是否可信
        {
            string realpassword = "";
            string sql = "select [uPwd] from [user] where uNum=?";
            var param = new OleDbParameter("@uNum", worknum);
            object result = helper.ExecuteScalar(constr, CommandType.Text, sql, param);
            if (result != null)
                realpassword = result.ToString();
            if (!String.IsNullOrEmpty(realpassword)) { return password.Equals(realpassword.Trim()); }
            else return false;
        }

        public static bool ChangeLogInfor(string worknum, DateTime time)//更新登录时间
        {
            string sql = "update [user] set lastLogin=? where uNum=?";
            var param = new OleDbParameter[] { 
                new OleDbParameter("@lastLogin",time.ToString("yyyy-MM-dd HH:mm:ss")),
                new OleDbParameter("@uNum",worknum)
            };
            return helper.ExecuteNonQuery(constr, CommandType.Text, sql, param) == 1 ? true : false;
        }
        public static bool ChangePwd(string pwd, string worknum)//更新密码
        {
            string sql = "update [user] set uPwd=? where uNum=?";
            var param = new OleDbParameter[] { 
                new OleDbParameter("@uPwd",pwd),
                new OleDbParameter("@uNum",worknum)
            };
            return helper.ExecuteNonQuery(constr, CommandType.Text, sql, param) == 1 ? true : false;
        }
        public static string GetUserID(string worknum)//由学号获得id
        {
            string sql = "select [uID] from [user] where uNum=?";
            var param = new OleDbParameter("@uNum", worknum);
            object result = helper.ExecuteScalar(constr, CommandType.Text, sql, param);
            return result == null ? "0" : result.ToString();
        }
        public static string GetUserName(string worknum)//由学号获得昵称
        {
            string sql = "select [uName] from [user] where uNum=?";
            var param = new OleDbParameter("@uNum", worknum);
            object result = helper.ExecuteScalar(constr, CommandType.Text, sql, param);
            return result == null ? "" : result.ToString();
        }
        public static string GetUserMail(string worknum)//由学号获得邮箱
        {
            string sql = "select [uMail] from [user] where uNum=?";
            var param = new OleDbParameter("@uNum", worknum);
            object result = helper.ExecuteScalar(constr, CommandType.Text, sql, param);
            return result == null ? "" : result.ToString();
        }
    }
}
