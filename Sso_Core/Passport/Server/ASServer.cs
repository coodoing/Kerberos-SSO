using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sso_Core.Interface.Server;
using Sso_Model;
using Sso_Core.Service;
using Sso_Core.Security;
using Sso_Core.Message;
using System.Web;

namespace Sso_Core.Passport.Server
{
    public class ASServer : IASServer
    {
        KeyType keyTp = new KeyType();   // 用来初始化的 不能去掉
        DESCryption desCryp = new DESCryption();
        RSACryption rsaCrpt = new RSACryption();

        string publicKey = "&lt;RSAKeyValue&gt;&lt;Modulus&gt;wYCBD0XgHQ6xUZguY4kcYlR5d5BJGu9RkXeGp+1Y1DGOL46rCQ1LkhJjPPTNOS7wchETKsGoutaojoI74Al/p6ZIRDD8b6SDaOBE1X0BFFle5DALtaTF7LblsP3p6TcTOJNb7LjbSyJrpiAtf5zREbGzLJfVRITyictXFOrRpLs=&lt;/Modulus&gt;&lt;Exponent&gt;AQAB&lt;/Exponent&gt;&lt;/RSAKeyValue&gt;";
        string privateKey = "&lt;RSAKeyValue&gt;&lt;Modulus&gt;wYCBD0XgHQ6xUZguY4kcYlR5d5BJGu9RkXeGp+1Y1DGOL46rCQ1LkhJjPPTNOS7wchETKsGoutaojoI74Al/p6ZIRDD8b6SDaOBE1X0BFFle5DALtaTF7LblsP3p6TcTOJNb7LjbSyJrpiAtf5zREbGzLJfVRITyictXFOrRpLs=&lt;/Modulus&gt;&lt;Exponent&gt;AQAB&lt;/Exponent&gt;&lt;P&gt;9SAz77XW+TJ7sSX5fsOH7hna48rND1qdl4qxgRIm42j+JufXmke2m5kXD33XAD2dhga8irqi4RRPGag19UhZDw==&lt;/P&gt;&lt;Q&gt;yhYHorGI/g/jtiyl75PWgFvumzUWAyfn4zxLYIrYx4rwDkW0y4zI0f+rn4NAEA5yMG6HGf5INOdGP6SGK1dBlQ==&lt;/Q&gt;&lt;DP&gt;x9PSww2oDEo9T7K3a8GWpGHrcVu4Q1YJtqpX/fKARU8oMSs7NghUMxBgHj7l8MpKCiKfvTBc68QUn8PjCOxLvQ==&lt;/DP&gt;&lt;DQ&gt;w5RskV2nCtPP+2jcs7Bt4c6Xb+kBw84kU7zv6qCWSxDBYd6+ql03ol4B+KArKR8CDrN514NM2L6YM1IEc/+/vQ==&lt;/DQ&gt;&lt;InverseQ&gt;yLGIg+mZO6tzp2dBOBastCFIe59QiU7Flo0kCes+iBglt0smP2QHM87GR/Hwtmrww6sFrEELpe0QNXLMUGx9rw==&lt;/InverseQ&gt;&lt;D&gt;D2mQ3gqOw0G8VedtKnhtTle0JRXU/NQ7Bv8iNpXtEVmxHZCbhukML+JPDTficL+QX0WE5U7vhZI7cr5kzn9zL1+sTAt1i/4cxAhiInQgiGDhmEKp0mfb/okHWQk4BxImG+JMh1eVnvwUIBGPm/Q1U+cTE07p3yW7fdTnhhL00WE=&lt;/D&gt;&lt;/RSAKeyValue&gt;";


        public ASServer()
        {

        }

        public User UserValidate(KerbASRequest kerbAsReq, ref string errorInfo, out TGTicket tgticket, out string kerbAsResponse)
        {
            User user = LoginService.GetUser(kerbAsReq.UserName, kerbAsReq.UserPwd);
            if (user == null)
            {
                errorInfo = "输入的用户名和密码错误";
                tgticket = null;
                kerbAsResponse = "";
                return null;
            }
            else
            {
                //TGTicket tgTicket = new TGTicket(user.UserCode);
                //string key1 = KeyType.Session_Key_1;  //Client与TGS的会话密钥
                //string iv = KeyType.Iv;  // Or "********"
                ////StringBuilder strBuilder = new StringBuilder();
                ////strBuilder.Append(tgTicket.Uid);
                ////strBuilder.Append("|");
                ////strBuilder.Append(tgTicket.TS2);
                ////strBuilder.Append("|");
                ////strBuilder.Append(tgTicket.LifeTime2);
                //string strBuilder1 = string.Concat(tgTicket.Uid, "|", Convert.ToString(tgTicket.TS2));
                //string strBuilder = string.Concat(strBuilder1, "|", Convert.ToString(tgTicket.LifeTime2));

                ////string[] strArray = encryptTicket.Split('|');  解密之后应有的操作
                //string encrypttgTicket = desCryp.Encrypt(strBuilder, KeyType.AS_TGS_Key, KeyType.Iv);  //TGTicket票据的加密           
                //string kerbAsResp = desCryp.Encrypt(string.Concat(string.Concat(encrypttgTicket, "|", desCryp.GenerateDesCryProvider(ref key1, ref iv)),"|",user.UserCode), KeyType.AS_Client_Key, KeyType.Iv);  // AS向Client发回的响应

                ////tgticket = des.Encrypt(tgTicket,key1,iv); 
                //tgticket = tgTicket;
                //kerbAsResponse = kerbAsResp;   //最终AS向Client发送的数据
                TGTicket tgTicket;
                kerbAsResponse = CreateKerbAsResp(user.UserCode, out tgTicket);
                tgticket = tgTicket;
                return user;
            }
        }

        public string CreateKerbAsResp(string uid, out TGTicket tgticket)
        {
            User user = LoginService.GetUser(uid);
            TGTicket tgTicket = new TGTicket(user.UserCode);
            string key1 = KeyType.Session_Key_1;  //Client与TGS的会话密钥
            string iv = KeyType.Iv;  //或者直接写成 "********"
            //StringBuilder strBuilder = new StringBuilder();
            //strBuilder.Append(tgTicket.Uid);
            //strBuilder.Append("|");
            //strBuilder.Append(tgTicket.TS2);
            //strBuilder.Append("|");
            //strBuilder.Append(tgTicket.LifeTime2);
            string strBuilder1 = string.Concat(tgTicket.Uid, "|", Convert.ToString(tgTicket.TS2));
            string strBuilder = string.Concat(strBuilder1, "|", Convert.ToString(tgTicket.LifeTime2));
            //string[] strArray = encryptTicket.Split('|');  // 解密之后应有的操作
            //string test = desCryp.GenerateDesCryProvider(ref key1, ref iv);  // 测试会话key的数据
            string encrypttgTicket = desCryp.Encrypt(strBuilder, KeyType.AS_TGS_Key, KeyType.Iv);  //TGTicket票据的加密  



            // 主要是确保随机密钥的安全性：下面就是数字签名的流程
            key1 = desCryp.GenerateDesCryProvider(ref key1, ref iv);
            //string key1text = rsaCrpt.RSAEncrypt(HttpUtility.HtmlDecode(publicKey), key1);
            string hashData = "";
            rsaCrpt.GetHash(key1, ref hashData);
            string rsasign = "";  //key1text的数字签名   
            rsaCrpt.SignatureFormatter(HttpUtility.HtmlDecode(privateKey), hashData, ref rsasign);
            string kerbAsResp = desCryp.Encrypt(string.Concat(string.Concat(encrypttgTicket, "|", key1), "|", user.UserCode), KeyType.AS_Client_Key, KeyType.Iv);
            kerbAsResp = string.Concat(kerbAsResp, ",", rsasign);


            //string kerbAsResp = desCryp.Encrypt(string.Concat(string.Concat(encrypttgTicket, "|", desCryp.GenerateDesCryProvider(ref key1, ref iv)), "|", user.UserCode), KeyType.AS_Client_Key, KeyType.Iv);  // AS向Client发回的响应
            tgticket = tgTicket;     //tgticket = des.Encrypt(tgTicket,key1,iv);  //tgticket不需要被加密处理
            return kerbAsResp;   //最终AS向Client发送的数据
        }
    }
}
