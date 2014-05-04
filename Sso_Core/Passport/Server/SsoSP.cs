using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Sso_Core.Interface.Server;
using Sso_Core.Security;
using Sso_Core.Helper;
using Sso_Core.Service;
using Sso_Core.Provider.Base;
using Sso_Model;
using System.Web.Security;

namespace Sso_Core.Passport.Server
{
    public class SsoSP : ISP
    {
        private string _Uid;
        private ISite _Site;

        #region SsoSP Member

        public string Uid
        {
            get { return this._Uid; }
        }

        public ISite Site
        {
            get { return this._Site; }
        }
        #endregion

        public SsoSP()
        {
            //产生转向的URL: http://localhost:5507/LoginIn.aspx?siteid =  &rsasigntext = timestamp=?& uid= &fromurlkry = 
            HttpContext currrent = HttpContext.Current;
            string siteid = currrent.Request.QueryString["siteid"];   //这里别画蛇添足，转换为ToString()，否则会抛出异常
            if (!string.IsNullOrEmpty(siteid))
            {
                this._Site = new SsoSite(siteid);
                //string timestamp = currrent.Request.QueryString["timestamp"];
                string uid = currrent.Request.QueryString[_Site.UidKey];   // 从应用服务站点传来的uid为空
                this._Uid = uid;
            }
        }

        /// <summary>
        ///  用于页面跳转控制
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="transferUrl"></param>
        public void PageTransfer(string uid, string transferUrl)
        {
            HttpContext current = HttpContext.Current;
            string url = "";
            // 如果是用户直接登录login.aspx，而且session中存在uid的话
            if ((this._Site == null) || string.IsNullOrEmpty(this._Site.SiteID))
            {
                url = UrlHelper.AddParam(UrlHelper.AddParam(transferUrl, "uid", uid), "timestamp", DateTime.Now.ToString());
                //url = transferUrl;
                current.Response.Redirect(url);
            }

            string backupUrl0 = current.Request.QueryString["fromurl"];
            if (string.IsNullOrEmpty(backupUrl0))
            {

            }
            RSACryption rsa = new RSACryption();

            string backupUrl = current.Server.UrlDecode(backupUrl0);
            string timestamp = DateTime.Now.ToString();

            // 跳转的时候将uid加密 这样在SsoCP中handlQueryString中进行uid的解密  保证uid 的安全传送

            string uidtext = rsa.RSAEncrypt(this._Site.PublicKey, uid);
            string hashData = "";
            rsa.GetHash(uidtext, ref hashData); //生成报文摘要
            string rsasign = "";  //uidtext的数字签名   
            rsa.SignatureFormatter(this._Site.PrivateKey, hashData, ref rsasign);  //this._Site.PrivateKey, hashData, ref rassign
            //url = UrlHelper.AddParam(UrlHelper.AddParam(UrlHelper.AddParam(backupUrl, "uid", uidtext), "rsauidsign", rsasign),"timestamp",timestamp);
            url = UrlHelper.AddParam(UrlHelper.AddParam(UrlHelper.AddParam(backupUrl, "uid", uid), "rsauidsign", rsasign), "timestamp", timestamp);
            current.Response.Redirect(url);
        }

        /// <summary>
        /// 该功能主要是用于:用户已登录过系统，然后忘了，直接进入登录页面，准备进行登录时候
        /// 做出的一个判断 ：：：检查用户的票据是否存在  
        /// </summary>
        /// <returns></returns>
        public string CheckExistUserTicket()
        {
            HttpContext current = HttpContext.Current;
            if (current.Session["uid"] != null)
            {
                //if (this._Site != null)
                //{
                //    SsoUserLoginList.Instance().AddUser(current.Session["uid"].ToString(), this._Site.SiteID);
                //}
                //if (current.Session.IsNewSession == false)  //注销功能能够实现，但是不能实现单点登录功能
                //{
                //    return null;
                //}
                if (LoginService.GetUser(current.Session["uid"].ToString()).IsLogin == true)  //注销之后，session值还可以获取，但是数据库中发生变化(标志该用户已经推出登录)
                {
                    return current.Session["uid"].ToString();
                }
                else
                {
                    return null;
                }




            }
            return null;
        }

        // 检查输入的用户是否存在  如果存在，则返回uid
        public string CheckExistUser(User user)
        {
            bool result = SsoProvider.Instance().Login(user.LoginName, user.Password);
            if (result)
            {
                return user.UserCode;
            }
            return "";
        }

        public void SaveUserTicket(string uid)
        {
            HttpContext.Current.Session["uid"] = uid;
        }

        // 在KDC认证中心的登出
        public void LogOut()
        {
            HttpContext current = HttpContext.Current;
            try
            {
                if (current.Session["uid"] != null)
                {
                    string str = current.Session["uid"].ToString(); //看先注销，后登陆的情况下session里面有值没有
                    current.Session.Remove("uid");
                    current.Session["uid"] = null;
                    FormsAuthentication.SignOut();
                    current.Session.Abandon();
                    current.Session.Clear();
                    current.Session.Contents.RemoveAll();
                    current.Session.RemoveAll();
                    current.Response.AddHeader("Refresh", "0");  // //刷新一下浏览器，把缓存清空。 另一种实现：Response.Redirect(Request.Url.ToString()); 

                    // 在清除之后的判断操作
                    if (current.Session["uid"] == null)
                    {
                        current.Response.Write("uid已经被删除");
                    }
                    else
                    {
                        current.Response.Write("Session[uid]的值是：" + current.Session["uid"].ToString());
                    }
                }
                else
                {
                    current.Session.Remove("uid");
                    current.Session["uid"] = null;
                    FormsAuthentication.SignOut();
                    current.Session.Abandon();
                    current.Session.Clear();
                    current.Session.Contents.RemoveAll();
                    current.Session.RemoveAll();
                    current.Response.AddHeader("Refresh", "0");  // //刷新一下浏览器，把缓存清空。 另一种实现：Response.Redirect(Request.Url.ToString()); 

                    current.Session.Timeout = 0;
                    // 在清除之后的判断操作
                    if (current.Session["uid"] == null)
                    {
                        current.Response.Write("uid已经被删除");
                    }
                    else
                    {
                        current.Response.Write("Session[uid]的值是：" + current.Session["uid"].ToString());
                    }
                }
            }
            catch (System.Exception ex)
            {

            }

        }
    }
}
