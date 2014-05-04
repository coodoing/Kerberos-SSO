using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sso_Model;
using Sso_Core.Provider.Base;

namespace Sso_Core.Provider.Base
{
    /// <summary>
    ///  可以把操作数据库的provider方法放入Interface接口中
    /// </summary>    
    public partial class SsoProvider : ProviderBase   //public class LoginProvider
    {
        /// <summary>
        /// 用于用户登录是否成功的判断
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public bool Login(string userName,string userPwd)
        {
            using (Kerberos_SsoEntities kerbSso = new Kerberos_SsoEntities())
            {
                var obj = from u in kerbSso.User
                          where u.LoginName == userName && u.Password == userPwd
                          select u;
                return obj == null ? false : true;
            }            
        }

        public User GetUser(string userName,string userPwd)
        {
            using (Kerberos_SsoEntities kerbSso = new Kerberos_SsoEntities())
            {
                var obj = from u in kerbSso.User
                          where u.LoginName == userName && u.Password == userPwd
                          select u;
                return obj == null ? null : obj.FirstOrDefault();
            }    
        }

        public User GetUser(string uid)
        {
            try
            {
                using (Kerberos_SsoEntities kerbSso = new Kerberos_SsoEntities())
                {
                    var user = from u in kerbSso.User
                               where u.UserCode == uid
                               select u;
                    return user == null ? null : user.FirstOrDefault();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Exception is :" + ex.Message);
            }
            return null;
        }

        /// <summary>
        ///  成功登录后修改数据库中的信息：如LastOS，lastIP
        /// </summary>
        /// <param name="user"></param>
        public bool UpdateUser(User user)
        {
            using(Kerberos_SsoEntities kerbSso = new Kerberos_SsoEntities())
            {
                User oldUser = kerbSso.User.FirstOrDefault(k => k.UserCode == user.UserCode);
                kerbSso.Attach(oldUser);
                kerbSso.ApplyPropertyChanges("User", user);
                return (kerbSso.SaveChanges() == 1);
            }
        }


    }
}
