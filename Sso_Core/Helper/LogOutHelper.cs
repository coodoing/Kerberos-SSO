using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Sso_Core.Interface.Client;
using Sso_Core.Passport.Client;
using Sso_Core.Interface.Server;
using Sso_Core.Passport.Server;
using System.Web;

namespace Sso_Core.Helper
{
    public class LogOutHelper
    {
        static protected ICP icp = new SsoCP();
        static protected ISP isp = new SsoSP();

        public static string LogOut(string nowUrl)
        {
            //icp.LogOut();
            //isp.LogOut();
            icp.LogOut(isp);
            string url = "";
            url = ReplaceToken(nowUrl);
            return url;
        }

        /// <summary>
        ///  将url中的uid去除
        /// </summary>
        /// <param name="nowUrl"></param>
        /// <returns></returns>
        public static string ReplaceToken(string nowUrl)
        {
            string url = nowUrl;
            url = Regex.Replace(url, @"(\?|&)uid=.*", "", RegexOptions.IgnoreCase);
            //return "http://localhost:5077/LoginIn.aspx?fromurl=" + HttpUtility.UrlEncode(url);
            return "http://localhost:5077/LoginIn.aspx";
        }
    }
}
