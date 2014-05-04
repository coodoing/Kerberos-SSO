using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Sso_Core.Interface.Client;

namespace Sso_Core.Passport.Client
{
    public class SsoUserStateList : IUserStateList
    {
        /*
         保证userList列表只允许一个线程【方法，如Add、Remove】操作
         * 保证Add的时候，不能GetUserStateList获取在线用户的数量
         */
        public List<string> userList = new List<string>(); 
        public static SsoUserStateList userStateListInstance = null;

        public static SsoUserStateList Instance()
        {
            if (userStateListInstance == null) 
            {
                userStateListInstance = new SsoUserStateList();
            }
            return userStateListInstance;
        }

        public void Add(string uid)
        {
            lock(userList)
            {
                bool exist = false;
                foreach (string u in userList)
                {
                    if (u == uid)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    userList.Add(uid);
                }
            }
        }

        public List<string> GetUserStateList()
        {
             lock(userList)
             {
                 return userList;
             }
        }

        public bool Check(string uid)
        {
            lock (userList)
            {
                bool exist = false;
                foreach (string u in userList)
                {
                    if (u == uid)
                    {
                        exist = true;
                    }
                }
                return exist;
            }
        }

        public void Remove(string uid)
        {
            lock(userList)
            {
                userList.Remove(uid);
            }
        }
    }
}
