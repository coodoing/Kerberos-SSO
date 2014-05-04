using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sso_Core.Security;

namespace Sso_Core.Security
{
    /// <summary>
    ///  目的就是为了保存和管理kerberos认证过程中涉及的Key 【密钥】
    /// </summary>
    public class KeyType
    {
        private string iv1 = "********";
        private string key1 = "********";

        private DESCryption desCry = new DESCryption();

        public static string AS_Client_Key = "11111111";    // 用户与AS认证服务器之间的密钥
        public static string AS_TGS_Key = "22222222";
        public static string Session_Key_1 = "";   // 用户与TGS服务器之间的会话密钥
        public static string TGS_AP_Key = "33333333";
        public static string Client_AP_Key = "";   // 用户与AP之间的会话密钥

        public static string Iv="********";

        // iv是保持不变的   而KeyType的初始化也决定了在这次会话过程中几个密钥的不变性
        public KeyType()
        {
            //Iv = iv1;
            //AS_Client_Key = desCry.GenerateDesCryProvider(ref key1, ref iv1);
            //Init();
            //AS_TGS_Key = desCry.GenerateDesCryProvider(ref key1, ref iv1);
            //Init();
            //Session_Key_1 = "";   // 临时通话密钥的产生
            //TGS_AP_Key = desCry.GenerateDesCryProvider(ref key1, ref iv1);
            //Client_AP_Key = "";
        }                     

        public void Init()
        {
            iv1 = "********";
        }

        #region   几个长期密钥Key的初始化
        #endregion
    }
}
