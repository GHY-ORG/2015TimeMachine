using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Net;
using System.Xml;

namespace Common
{
    public class CheckNum
    {
        public static bool CheckUserNum(string userNum, string password)
        {
            string Url = @"http://v.ghy.swufe.edu.cn/Service.asmx/chkUser?userNum=" + userNum + "&password=" + password;
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            byte[] dataBuffer = wc.DownloadData(Url);
            string strWebData = Encoding.Default.GetString(dataBuffer);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strWebData);
            string a = doc.GetElementsByTagName("int")[0].InnerText;
            if (a.Equals("2"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
