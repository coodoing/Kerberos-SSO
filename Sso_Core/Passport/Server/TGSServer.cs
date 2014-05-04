using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sso_Core.Interface.Server;
using Sso_Core.Security;
using Sso_Core.Message;
using System.Web;

namespace Sso_Core.Passport.Server
{
    public class TGSServer : ITGSServer
    {
        private DESCryption desCrypt = new DESCryption();
        private RSACryption rsaCrpt = new RSACryption();
        private KerbTGSRequest kerbTgsReq;

        string publicKey = "&lt;RSAKeyValue&gt;&lt;Modulus&gt;wYCBD0XgHQ6xUZguY4kcYlR5d5BJGu9RkXeGp+1Y1DGOL46rCQ1LkhJjPPTNOS7wchETKsGoutaojoI74Al/p6ZIRDD8b6SDaOBE1X0BFFle5DALtaTF7LblsP3p6TcTOJNb7LjbSyJrpiAtf5zREbGzLJfVRITyictXFOrRpLs=&lt;/Modulus&gt;&lt;Exponent&gt;AQAB&lt;/Exponent&gt;&lt;/RSAKeyValue&gt;";
        string privateKey = "&lt;RSAKeyValue&gt;&lt;Modulus&gt;wYCBD0XgHQ6xUZguY4kcYlR5d5BJGu9RkXeGp+1Y1DGOL46rCQ1LkhJjPPTNOS7wchETKsGoutaojoI74Al/p6ZIRDD8b6SDaOBE1X0BFFle5DALtaTF7LblsP3p6TcTOJNb7LjbSyJrpiAtf5zREbGzLJfVRITyictXFOrRpLs=&lt;/Modulus&gt;&lt;Exponent&gt;AQAB&lt;/Exponent&gt;&lt;P&gt;9SAz77XW+TJ7sSX5fsOH7hna48rND1qdl4qxgRIm42j+JufXmke2m5kXD33XAD2dhga8irqi4RRPGag19UhZDw==&lt;/P&gt;&lt;Q&gt;yhYHorGI/g/jtiyl75PWgFvumzUWAyfn4zxLYIrYx4rwDkW0y4zI0f+rn4NAEA5yMG6HGf5INOdGP6SGK1dBlQ==&lt;/Q&gt;&lt;DP&gt;x9PSww2oDEo9T7K3a8GWpGHrcVu4Q1YJtqpX/fKARU8oMSs7NghUMxBgHj7l8MpKCiKfvTBc68QUn8PjCOxLvQ==&lt;/DP&gt;&lt;DQ&gt;w5RskV2nCtPP+2jcs7Bt4c6Xb+kBw84kU7zv6qCWSxDBYd6+ql03ol4B+KArKR8CDrN514NM2L6YM1IEc/+/vQ==&lt;/DQ&gt;&lt;InverseQ&gt;yLGIg+mZO6tzp2dBOBastCFIe59QiU7Flo0kCes+iBglt0smP2QHM87GR/Hwtmrww6sFrEELpe0QNXLMUGx9rw==&lt;/InverseQ&gt;&lt;D&gt;D2mQ3gqOw0G8VedtKnhtTle0JRXU/NQ7Bv8iNpXtEVmxHZCbhukML+JPDTficL+QX0WE5U7vhZI7cr5kzn9zL1+sTAt1i/4cxAhiInQgiGDhmEKp0mfb/okHWQk4BxImG+JMh1eVnvwUIBGPm/Q1U+cTE07p3yW7fdTnhhL00WE=&lt;/D&gt;&lt;/RSAKeyValue&gt;";


        public TGSServer()
        {

        }

        /*
         * 
         * TGT解密的过程不是在：Client端被解密，而是在TGSServer端被解密
         * 
         * TGS服务器处理KerbTGSRequest请求,并判断TGTicket是否有效；
         * 有效的话，则直接产生STicket
         * **/
        public void HandleTgsReq(KerbTGSRequest kerbTgsRequest, out bool tgsvalid, out string kerbTgsResponse)
        {

            // out型参数可以不被初始化
            string session_key_1 = kerbTgsRequest.session_key_1;
            string encryptUid = kerbTgsRequest.encryptUid;
            //TGTicket tgticket = kerbTgsRequest.tgticket;
            string encrptTgsTicket = kerbTgsRequest.encyptgsTicket;

            string tgticket = desCrypt.Decrypt(encrptTgsTicket, KeyType.AS_TGS_Key, KeyType.Iv);
            string[] ticketArray = tgticket.Split('|');
            string uid = ticketArray[0];
            DateTime ts2 = Convert.ToDateTime(ticketArray[1]);
            double lifetime2 = Convert.ToDouble(ticketArray[2]);

            TGTicket tgtTicket = new TGTicket(uid, ts2, lifetime2);

            string validUid = desCrypt.Decrypt(encryptUid, session_key_1, KeyType.Iv);

            string errorInfo = string.Empty;
            if ((!validUid.Equals(uid)) || (string.IsNullOrEmpty(validUid)))
            {
                errorInfo = "TGS票据被修改,uid的值已经被改变";
                tgsvalid = false;
                kerbTgsResponse = "";
                HandleTgsReq(kerbTgsRequest, out tgsvalid, out kerbTgsResponse);
            }
            else if (IsTGTExpired(tgtTicket))
            {
                errorInfo = "TGS票据过期";
                tgsvalid = false;
                kerbTgsResponse = "";
                HandleTgsReq(kerbTgsRequest, out tgsvalid, out kerbTgsResponse); //重新获取tgsResp请求
            }
            else
            {
                // 下面就是TGS服务器向Client发送的  uid与STicket中的uid是一样的
                STicket sticket = new STicket(uid);
                string key1 = KeyType.Client_AP_Key;  //Client与AP应用服务之间的会话密钥
                string iv = KeyType.Iv;  // Or "********"
                string strBuilder1 = string.Concat(sticket.STIdentity, "|", Convert.ToString(sticket.TS4));
                string strBuilder2 = string.Concat(strBuilder1, "|", Convert.ToString(sticket.LifeTime4));
                string strBuilder3 = string.Concat(strBuilder2, "|", uid);
                string strBuilder = string.Concat(strBuilder3, "|", Convert.ToString(sticket.Adc1));

                string encryptSticket = desCrypt.Encrypt(strBuilder, KeyType.TGS_AP_Key, KeyType.Iv);  //加密过后的STicket
                // 下面实现一个时间戳验证
                //DateTime ts4_1 = DateTime.Now;           
                //string test = desCrypt.GenerateDesCryProvider(ref key1,ref iv);


                // 主要是确保随机密钥的安全性：下面就是数字签名的流程
                key1 = desCrypt.GenerateDesCryProvider(ref key1, ref iv);//desCrypt.GenerateDesCryProvider(ref key1, ref iv);
                //string key1text = rsaCrpt.RSAEncrypt(HttpUtility.HtmlDecode(publicKey), key1);
                string hashData = "";
                rsaCrpt.GetHash(key1, ref hashData);
                string rsasign = "";  //key1text的数字签名   
                rsaCrpt.SignatureFormatter(HttpUtility.HtmlDecode(privateKey), hashData, ref rsasign);

                string kerbTgsResp = string.Concat(string.Concat(encryptSticket, "|", key1), "|", uid);  // AS向Client发回的响应
                //kerbTgsResp = string.Concat(kerbTgsResp, ",", rsasign);
                tgsvalid = true;
                kerbTgsResponse = kerbTgsResp;

            }
        }

        public bool IsTGTExpired(TGTicket tgticket)
        {
            DateTime now = DateTime.Now;
            TimeSpan span = (TimeSpan)(now - tgticket.TS2);
            if (span.TotalMinutes > tgticket.LifeTime2)
            {
                return true;   // 票据过期
            }
            return false;
        }

    }
}
