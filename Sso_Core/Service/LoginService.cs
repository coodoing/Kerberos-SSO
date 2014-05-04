using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sso_Model;
using Sso_Core.Provider.Base;

namespace Sso_Core.Service
{
    public static class LoginService
    {
        public static bool Login(string userName,string userPwd)
        {
            return SsoProvider.Instance().Login(userName, userPwd);         
        }

        public static User GetUser(string userName, string userPwd)
        {
            return SsoProvider.Instance().GetUser(userName, userPwd);
        }

        public static User GetUser(string uid)
        {
            return SsoProvider.Instance().GetUser(uid);
        }

        public static bool UpdateUser(User user)
        {
            return SsoProvider.Instance().UpdateUser(user);
        }
    }
}
