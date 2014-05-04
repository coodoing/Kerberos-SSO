using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sso_Core.Interface.Server;

namespace Sso_Core.Interface.Client
{
    /// <summary>
    ///  ClientPassort的接口，用于发出登录请求和登出请求   【作判断，看用户是否已登录】
    /// </summary>
    public interface ICP
    {
        string SiteCode { get; }
        string LoginAddress { get; }
        string LogOutAddress {  get; }
        string Uid { get; }   //用户名
        int TimeOut { get; }
        string UidField {  get; }
        string FromUrlField { get; }

        bool CheckUserState();
        bool Login(out string exInfo);
        void LogOut();
        void LogOut(ISP sp);
    }
}
