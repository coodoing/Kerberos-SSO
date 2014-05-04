using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sso_Core.Interface.Client
{
    /// <summary>
    /// 主要是在各个站点中：记录和维护在线联盟用户信息
    /// </summary>
    public interface IUserStateList
    {
        void Add(string uid);
        bool Check(string uid);
        void Remove(string uid);
        List<string> GetUserStateList();        
    }
}
