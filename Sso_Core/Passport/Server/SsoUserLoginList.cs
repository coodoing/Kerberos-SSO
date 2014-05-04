using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Sso_Core.Interface.Server;

namespace Sso_Core.Passport.Server
{
    public class SsoUserLoginList : IUserLoginList
    {
        // HashTable的优点就在于其索引的方式，速度非常快  
        // Hashtable 的键值对属于DictionaryEntry类型
        // HashTable 获取元素的方法是使用“键”访问键所对应的值，即HashTable[key]。

        // 登录登出操作修改hashTable比较频繁 ，保证了线程的安全性
        public static Hashtable hashTable = new Hashtable();

        public static SsoUserLoginList userLoginListInstance = null;

        public static SsoUserLoginList Instance()
        {
            if (userLoginListInstance == null)
            {
                userLoginListInstance = new SsoUserLoginList();
            }
            return userLoginListInstance;
        }
        public SsoUserLoginList()
        {

        }        

        public void AddUser(string uid, string siteid)
        {
            // 在Hashtable中，一个uid可以对应很多siteid。而这些siteid组合成一个List<string>类型
            lock (hashTable)    
            {
                List<string> sites = new List<string>();
                if (hashTable.Contains(uid))  // 判断HashTable中是否包含指定“键”。
                {
                    sites = hashTable[uid] as List<string>; // HashTable 获取元素的方法是使用“键”访问键所对应的值，即HashTable[key]。
                }
                if (sites.Contains(siteid))
                {
                    return;
                }
                sites.Add(siteid);
                hashTable[uid] = sites;
            }
        }
        public void RemoveUser(string uid)
        {
            lock(hashTable)
            {
                hashTable.Remove(uid);
            }
        }
        public List<string> GetUserLoginList(string uid)
        {
            lock(hashTable)
            {
                List<string> sites = new List<string>();
                if (hashTable.Contains(uid))
                {
                    sites = hashTable[uid] as List<string>;
                }
                return sites;
            }
        }
    }
}
