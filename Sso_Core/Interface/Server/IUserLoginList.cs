using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sso_Core.Interface.Server
{
    /// <summary>
    /// IUserLoginList是：    在KDC【认证中心】描述和操作用户登录站点清单的接口
    /// </summary>
    public interface IUserLoginList
    {
        void AddUser(string uid,string siteid);
        void RemoveUser(string uid);
        List<string> GetUserLoginList(string uid);
    }
}
