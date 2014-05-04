using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sso_Core.Interface.Server
{
    /// <summary>
    /// IUser是用于描述和操作联盟用户的接口
    /// </summary>
    public interface IUser
    {
        string Uid { get; set; }
        string Upwd { get; set; }
        void Register();
        bool Validate();
    }
}
