using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sso_Core.Interface.Server
{
    /// <summary>
    /// ISite是用于描述和操作成员站点的接口
    /// </summary>
    public interface ISite
    {
        string SiteID { get; set; }
        string HomePage { set; get; }
        string LogOutUrl { set; get; }
        string PublicKey { set; get; }
        string PrivateKey { set; get; }
        string FromUrlKey { set; get; }
        string UidKey { set; get; }

        void Add();
    }
}
