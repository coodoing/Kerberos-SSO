using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Sso_Core.Helper;
using Sso_Core.Interface.Client;
using Sso_Core.Passport.Client;
using Sso_Core.Security;
using Sso_Core.Helper;
using Sso_Core.Enumeration;
using Sso_Core.Interface.Server;
using Sso_Core.Service;
using Sso_Model;

namespace Sso_Core.Passport.Client
{
    public class SsoCP : ICP
    {
        private string m_SiteCode = string.Empty;
        private int m_TimeOut;
        private string m_Uid = string.Empty;
        private string m_UidFiled = "uid";
        private string m_FromUrlField = "fromurl";

        /*登录登出地址*/
        private string m_LoginAddress = string.Empty;
        private string m_LogoutAddress = string.Empty;
        /*RSA加密的公钥和私钥*/
        private string m_PrivateKey = string.Empty;
        private string m_PublicKey = string.Empty;

        //上转型
        private IUserStateList m_UserStateList = SsoUserStateList.Instance();
        private RSACryption m_Rsa = new RSACryption();

        public SsoCP()
        {
            m_LoginAddress = SsoSettingsHelper.Instance().LoginInUrl;
            m_LogoutAddress = SsoSettingsHelper.Instance().LogOutUrl;
            m_PrivateKey = SsoSettingsHelper.Instance().PrivateKey;
            m_PublicKey = SsoSettingsHelper.Instance().PublicKey;
            m_SiteCode = SsoSettingsHelper.Instance().SiteCode;
            m_TimeOut = SsoSettingsHelper.Instance().TimeOut;
        }

        #region SsoCP Members
        public string LoginAddress
        {
            get
            {
                return this.m_LoginAddress;
            }
        }

        public string LogOutAddress
        {
            get
            {
                return this.m_LogoutAddress;
            }
        }
        public string UidField
        {
            get
            {
                return this.m_UidFiled;
            }
        }

        public string FromUrlField
        {
            get
            {
                return this.m_FromUrlField;
            }
        }


        public string PrivateKey
        {
            get
            {
                return this.m_PrivateKey;
            }
        }

        public string SiteCode
        {
            get
            {
                return this.m_SiteCode;
            }
        }

        public int TimeOut
        {
            get
            {
                return this.m_TimeOut;
            }
        }

        public string Uid
        {
            get
            {
                return this.m_Uid;
            }
        }
        #endregion

        #region
        /// <summary>
        /// 检查用户是否已经登录过系统
        /// </summary>
        /// <returns></returns>
        public bool CheckUserState()
        {
            HttpContext current = HttpContext.Current;
            bool result = false;
            if (current.Session["uid"] != null)
            {
                this.m_Uid = current.Session["uid"].ToString();
                result = true;
            }
            //if (current.Request["uid"]!=null)
            //{
            //    this.m_Uid = current.Request.QueryString["uid"].ToString();
            //    result = true;
            //}
            //if ((current.Request.QueryString["uid"])!=null && (current.Session["uid"]!=null))
            //{
            //    if (current.Request.QueryString["uid"].ToString().Equals(current.Session["uid"]))
            //    {
            //        this.m_Uid = current.Request.QueryString["uid"].ToString();
            //        result = true;
            //    }
            //    else
            //    {
            //        current.Response.Write("uid数据被篡改");
            //        result = false ;
            //    }
            //}
            return result;
        }

        /// <summary>
        ///  User登录系统
        /// </summary>
        /// <param name="hintInfo">提示信息</param>
        /// <returns></returns>
        public bool Login(out string hintInfo)
        {
            HttpContext current = HttpContext.Current;
            string str = current.Request.Url.AbsoluteUri;
            hintInfo = "";
            /*检查如果session中无值，则检查请求字符串中是否存在uid，
             * 如果不存在则转向登录页面
             * 如果存在则从中解析出用户的guid，并存入session中*/
            if (!this.CheckUserState())
            {
                if ((current.Request.QueryString["uid"] == null) || (current.Request.QueryString["uid"] == ""))
                {
                    this.UserToLogin();
                }

                string identity = "";
                // 用来处理从KDC传来的uid，从而进行跳转以及用户状态的保存
                if (!this.HandleQueryString(out identity, out hintInfo))
                {
                    return false;
                }
                // 是应用服务处理好用户查询字符串之后，向session中保存的uid
                this.SaveUserIndentity(identity);
            }

            return true;
        }

        public void UserToLogin()
        {
            HttpContext current = HttpContext.Current;
            string str = this.m_SiteCode;
            string strHashData = "";
            this.m_Rsa.GetHash(str, ref strHashData);  // 获得hash描述的摘要
            string str3 = "";
            this.m_Rsa.SignatureFormatter(this.m_PrivateKey, strHashData, ref str3);  // Rsa的数字签名
            // 产生转向的URL: http://localhost:5507/LoginIn.aspx?siteid =  &rsasigntext = timestamp=?& uid= &fromurlkry = 
            //string url = UrlHelper.AddParam(UrlHelper.AddParam(UrlHelper.AddParam(UrlHelper.AddParam(this.m_LoginAddress, "siteid", this.m_SiteCode), "rsasigntext", str3), this.m_FromUrlField, current.Request.Url.ToString()), "timestamp", DateTime.Now.ToString());

            string url = UrlHelper.AddParam(UrlHelper.AddParam(UrlHelper.AddParam(this.m_LoginAddress, "siteid", this.m_SiteCode), "rsasigntext", str3), this.m_FromUrlField, current.Request.Url.ToString());

            current.Response.Redirect(url);
        }

        /// <summary>
        ///  保存用户id
        /// </summary>
        /// <param name="userIdentity"></param>
        public void SaveUserIndentity(string userIdentity)   // 即Uid
        {
            HttpContext current = HttpContext.Current;
            current.Session["uid"] = userIdentity;
            this.m_Uid = current.Session["uid"].ToString();
            //this.m_UserStateList.Add(userIdentity);
        }

        /// <summary>
        /// 用户访问服务应用站点
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="hintInfo"></param>
        /// <returns></returns>
        public bool HandleQueryString(out string identity, out string hintInfo)
        {
            HttpContext current = HttpContext.Current;
            identity = "";
            hintInfo = "";
            string uId = current.Request.QueryString["uid"].ToString();
            string timestamp = current.Request.QueryString["timestamp"];

            /**
               这里做了略微的调整 ：
             * 用户直接在LoginIn.aspx登录的时候，保存uid成功后直接将uid作为查询字符串传值过来 
             * 而不是之前的跳转到LibHomePage.aspx中，然后再进行登录判断
             * 
             * 用户在直接登录LibHomePage.aspx的时候，就按以前的步骤进行
             */


            //把用户名用PrivateKey进行rsa解密
            //string uid = this.m_Rsa.RSADecrypt(this.m_PrivateKey, uId);   
            // string uidsign = current.Request.QueryString["rsauidsign"];
            //if (string.IsNullOrEmpty(uidsign))
            //{
            //    hintInfo = "票据非法";
            //    return false;
            //}
            //数字签名的流程
            //string strHash = uId;
            //string strHashData = "";
            //this.m_Rsa.GetHash(strHash, ref strHashData);  // 获得hash描述的摘要
            //if (!this.m_Rsa.SignatureDeformatter(this.m_PublicKey,strHashData,uidsign))
            //{
            //    hintInfo = "数据被篡改，数据签名失败";
            //    return false;
            //}
            //identity = uid;

            DateTime ticketTimeStart = Convert.ToDateTime(timestamp);  // 判断 ”网址中“ 两个时间差
            TimeSpan span = (TimeSpan)(DateTime.Now - ticketTimeStart);
            if (span.TotalMinutes > this.m_TimeOut)
            {
                hintInfo = "票据已经过期";
                return false;
            }

            if (LoginService.GetUser(HttpContext.Current.Request.QueryString["uid"].ToString()).IsLogin == true)
            {
                identity = uId;
                return true;
            }
            if (LoginService.GetUser(HttpContext.Current.Request.QueryString["uid"].ToString()).IsLogin == false)
            {
                hintInfo = "票据已经过期";
                return false;
            }

            identity = "";
            return false;
        }

        public void LogOut()
        {
            HttpContext current = HttpContext.Current;
            try
            {
                string strUd = current.Session["uid"].ToString(); //看先注销，后登陆的情况下session里面有值没有



                /**
                 * 
                 * 在http://localhost:2523/LibHomePage.aspx  页面上点击注销的时候，发生的事件是:
                 * LogOut(ISP sp) 中因为没有timestamp的querystring，所以又会跳转到http://localhost:2523/LibHomePage.aspx  这个页面上
                 * 并触发AuthBase事件，最后变成的网页为： http://localhost:2523/LibHomePage.aspx?uid=admin&timestamp=2010%2f5%2f23+13%3a15%3a19

                 * **/

                string timestamp = current.Request.QueryString["timestamp"];
                DateTime dt = Convert.ToDateTime(timestamp);
                string timestr = dt.AddMinutes(-this.m_TimeOut).ToString();
                //dt.AddMinutes(this.m_TimeOut);
                //string Ud = current.Session["uid"].ToString();                              
                // string url = UrlHelper.AddParam(UrlHelper.AddParam("http://localhost:2523/LibHomePage.aspx", "uid", ""), "timestamp", timestr);


                string url = UrlHelper.AddParam(UrlHelper.AddParam("http://localhost:2523/LibHomePage.aspx", "uid", strUd), "timestamp", timestr);

                current.Session.Remove("uid");
                current.Session["uid"] = null;
                FormsAuthentication.SignOut();
                current.Session.Abandon();
                current.Session.Clear();
                current.Session.Contents.RemoveAll();
                current.Session.RemoveAll();
                current.Response.AddHeader("Refresh", "0");
                if (current.Session["uid"] == null)
                {
                    current.Response.Write("uid已经被删除");
                }
                else
                {
                    current.Response.Write("Session[uid]的值是：" + current.Session["uid"].ToString());
                }

                current.Response.Redirect(url);
            }
            catch (System.Exception ex)
            {

            }




        }



        public void LogOut(ISP sp)
        {
            HttpContext current = HttpContext.Current;
            try
            {


                string strUd = current.Session["uid"].ToString(); //看先注销，后登陆的情况下session里面有值没有
                string timestamp = current.Request.QueryString["timestamp"];
                DateTime dt = Convert.ToDateTime(timestamp);
                string timestr = dt.AddMinutes(-this.m_TimeOut).ToString();
                //dt.AddMinutes(this.m_TimeOut);
                //string Ud = current.Session["uid"].ToString();
                //string url = UrlHelper.AddParam(UrlHelper.AddParam("http://localhost:2523/LibHomePage.aspx", "uid", ""), "timestamp", timestr);

                string url = UrlHelper.AddParam(UrlHelper.AddParam("http://localhost:2523/LibHomePage.aspx", "uid", strUd), "timestamp", timestr);

                //string url = UrlHelper.AddParam("http://localhost:2523/LibHomePage.aspx", "timestamp", timestr);

                //bool islogin LoginService.GetUser(strUd);
                User user = LoginService.GetUser(strUd);
                user.IsLogin = false; // 说明用户的状态时没有登录的状态
                LoginService.UpdateUser(user);

                current.Session.Remove("uid");
                current.Session["uid"] = null;
                FormsAuthentication.SignOut();
                current.Session.Abandon();
                current.Session.Clear();
                current.Session.Contents.RemoveAll();
                current.Session.RemoveAll();
                current.Response.AddHeader("Refresh", "0");

                //current.Session.Timeout = 0;

                sp.LogOut();
                if (current.Session["uid"] == null)
                {
                    current.Response.Write("uid已经被删除");
                }
                else
                {
                    current.Response.Write("Session[uid]的值是：" + current.Session["uid"].ToString());
                }

                current.Response.Redirect(url);
            }
            catch (System.Exception ex)
            {

            }






        }


        #endregion

        #region  跨浏览器的单点登录
        // public bool Login(out string hintInfo)
        // {
        //     if (!CheckUserState())
        //     {

        //     }
        // }

        // public bool CheckUserState()
        // {
        //     LoginStatus status = new LoginStatus();

        // }

        //public void UserToLogin()
        //{

        //}

        // public bool HandleQueryString(out string identity,out string hintInfo)
        // {

        // }

        //public void SaveUserIndentity(string userIdentity) 
        //{

        //}

        //public void LogOut()
        //{

        //}
        #endregion

    }
}