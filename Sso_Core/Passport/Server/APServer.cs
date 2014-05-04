using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sso_Core.Interface.Server;
using Sso_Core.Security;
using Sso_Core.Message;

namespace Sso_Core.Passport.Server
{
    /// <summary>
    ///  应用服务器处理
    /// </summary>
    public class APServer :IAPServer
    {
        private DESCryption desCrypt = new DESCryption();
        private KerbAPRequest kerbApReq;

        public APServer()
        {
         
        }

        // 处理用户Client发送来的请求 ，并作出相关的处理
        public void HandleApReq(KerbAPRequest kerbApRequest,out bool stvalid , out bool apResponse)
        {
            string errorInfo = string.Empty;
            //STicket sticket = kerbApRequest.sticket;
            string encryptSTicket = kerbApRequest.encrptSticket;
            string sticket = desCrypt.Decrypt(encryptSTicket, KeyType.TGS_AP_Key, KeyType.Iv);
            string[] ticketArray = sticket.Split('|');
            string sIdentity = ticketArray[0];
            DateTime ts4 = Convert.ToDateTime(ticketArray[1]);
            double lifetime4 = Convert.ToDouble(ticketArray[2]);
            string uid2 = ticketArray[3];
            int adc = Convert.ToInt32(ticketArray[4]);

            STicket sTicket =  new STicket(uid2,sIdentity,ts4,adc,lifetime4);

            Authenticator authen = kerbApRequest.authenticator;
            if ((authen.adc != sTicket.Adc1) || (authen.uid != sTicket.Uid))
            {
                // 两者之间的认证标志不一样或者UID不一样的话   说明STicket被修改
                stvalid = false;
                apResponse = false;
                errorInfo = "STikcet票据已经被修改";
            }
            stvalid = true;    //票据是合法的
            // 下面就是对权限的管理和控制
            apResponse = true;   // 允许Client访问服务。
        }
    }
}
