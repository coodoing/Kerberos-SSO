using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using Sso_Core.Interface.Client;
using Sso_Core.Passport.Client;
using Sso_Core.Interface.Server;
using Sso_Core.Passport.Server;

//////////////////////////////////////////////////////////////////////////
/**
 在用户登录系统的整个执行过程期间，Uid用户code是一个凭证
 */
namespace Sso_Core.Web
{
    /// <summary>
    /// asp.net中Page和该Page中的UserControl控件,OnInit,OnLoad 事件的加载顺序
    ///正确的顺序是：UserControl控件OnInit--->Page中的OnInit--->Page中的OnLoad--->UserControl控件中的OnLoad
    /// 页面周期的过程是  OnInit->OnLoad->OnPreRender->OnRender 
    /// </summary>
    public class AuthBase : System.Web.UI.Page
    {
        protected ICP icp = new SsoCP();
        protected ISP isp = new SsoSP();

        private string personalCode;
        public string PersonalCode
        {
            set { this.personalCode = value; }
            get { return personalCode; }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                /*ASP.NET 的页面执行 PostBack 动作时，页面由伺服端重新传给客户端，而页面的垂直滚动条会跳回最上方，水平滚动条会跳回最左方。
为了解决此情形，只要将 Page 的MaintainScrollPositionOnPostBack 属性设为True 时，页面就会自动维护滚动条位置
             * **/
                MaintainScrollPositionOnPostBack = true;
                string hintInfo = "";
                //Response.Redirect("Failure.aspx");
                bool result = icp.Login(out hintInfo);
                if (!result)
                {
                    Response.Write("错误提示信息：" + hintInfo);
                    Response.Write("<br/>");
                    Response.Write("账户已经注销，请重新登录");
                    Response.Write("<br/>");
                    Response.Write("<a href=\"http://localhost:5077/LoginIn.aspx"+"\">返回登录页面重新登录"+"</a>");
                    Response.End();
                }
                else
                {
                    //personalCode = icp.Uid;  // 用户code   ，是作为用户UID的一个凭证
                    ////FormsAuthentication.RedirectFromLoginPage(icp.Uid, true);
                    //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    //  1, // 版本号。 
                    //    icp.Uid, // 与身份验证票关联的用户名。 
                    //  DateTime.Now, // Cookie 的发出时间。 
                    //  DateTime.Now.AddMinutes(23),// Cookie 的到期日期。 
                    //  false,       // 如果 Cookie 是持久的，为 true；否则为 false。 
                    //  "UserData"); // 将存储在 Cookie 中的用户定义数据。   roles是一个角色字符串数组 　                
                    //string encryptedTicket = FormsAuthentication.Encrypt(authTicket); //加密 
                    //// 存入Cookie 
                    //HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    //Response.Cookies.Add(authCookie);
                }
            }

            base.OnLoad(e);
        }
    }
}
