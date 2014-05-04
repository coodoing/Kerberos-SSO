using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sso_Core.Interface.Client;
using Sso_Model;

namespace Sso_Core.Interface.Server
{
    /// <summary>
    /// ServerPassport接口 ISP是认证中心KDC处理登录请求和登出请求的接口
    /// </summary>
    public interface ISP
    {
        ISite Site {get; }
        string Uid {get; }

        void LogOut();
        void SaveUserTicket(string uid);  // 保存用户的Ticket
        void PageTransfer(string uid, string transferUrl);   // 用于在kdc中进行页面跳转控制
        string CheckExistUserTicket(); // 检查用户的票据是否存在
        string CheckExistUser(User user); // 检查输入的用户是否存在
    }
}
