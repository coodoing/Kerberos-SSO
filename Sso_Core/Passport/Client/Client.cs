using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Sso_Core.Passport.Server;
using Sso_Core.Security;
using Sso_Core.Message;
using Sso_Model;


namespace Sso_Core.Passport.Client
{
    // Client主要是负责与ASServer以及TGSServer的交互
    public class Client
    {

        private DESCryption desCryp = new DESCryption();
        private RSACryption rsaCryp = new RSACryption();
        private KeyType keyTy = new KeyType();  //key密钥的初始化

        private ASServer asServer = new ASServer();  //注意这里不要用上转型对象。因为IASServer中没有一个函数
        private TGSServer tgServer = new TGSServer();
        private APServer apServer = new APServer();

        private TGTicket tgTicket;
        private STicket sTicket;
        private Authenticator auth;
        private string kerbAsResponse;
        private string errorInfo = "";

        private KerbTGSRequest kerbTgsReq;
        private KerbAPRequest kerbApReq;
        private KerbASRequest kerbAsReq;

        private string publicKey = "&lt;RSAKeyValue&gt;&lt;Modulus&gt;wYCBD0XgHQ6xUZguY4kcYlR5d5BJGu9RkXeGp+1Y1DGOL46rCQ1LkhJjPPTNOS7wchETKsGoutaojoI74Al/p6ZIRDD8b6SDaOBE1X0BFFle5DALtaTF7LblsP3p6TcTOJNb7LjbSyJrpiAtf5zREbGzLJfVRITyictXFOrRpLs=&lt;/Modulus&gt;&lt;Exponent&gt;AQAB&lt;/Exponent&gt;&lt;/RSAKeyValue&gt;";
        private string privateKey = "&lt;RSAKeyValue&gt;&lt;Modulus&gt;wYCBD0XgHQ6xUZguY4kcYlR5d5BJGu9RkXeGp+1Y1DGOL46rCQ1LkhJjPPTNOS7wchETKsGoutaojoI74Al/p6ZIRDD8b6SDaOBE1X0BFFle5DALtaTF7LblsP3p6TcTOJNb7LjbSyJrpiAtf5zREbGzLJfVRITyictXFOrRpLs=&lt;/Modulus&gt;&lt;Exponent&gt;AQAB&lt;/Exponent&gt;&lt;P&gt;9SAz77XW+TJ7sSX5fsOH7hna48rND1qdl4qxgRIm42j+JufXmke2m5kXD33XAD2dhga8irqi4RRPGag19UhZDw==&lt;/P&gt;&lt;Q&gt;yhYHorGI/g/jtiyl75PWgFvumzUWAyfn4zxLYIrYx4rwDkW0y4zI0f+rn4NAEA5yMG6HGf5INOdGP6SGK1dBlQ==&lt;/Q&gt;&lt;DP&gt;x9PSww2oDEo9T7K3a8GWpGHrcVu4Q1YJtqpX/fKARU8oMSs7NghUMxBgHj7l8MpKCiKfvTBc68QUn8PjCOxLvQ==&lt;/DP&gt;&lt;DQ&gt;w5RskV2nCtPP+2jcs7Bt4c6Xb+kBw84kU7zv6qCWSxDBYd6+ql03ol4B+KArKR8CDrN514NM2L6YM1IEc/+/vQ==&lt;/DQ&gt;&lt;InverseQ&gt;yLGIg+mZO6tzp2dBOBastCFIe59QiU7Flo0kCes+iBglt0smP2QHM87GR/Hwtmrww6sFrEELpe0QNXLMUGx9rw==&lt;/InverseQ&gt;&lt;D&gt;D2mQ3gqOw0G8VedtKnhtTle0JRXU/NQ7Bv8iNpXtEVmxHZCbhukML+JPDTficL+QX0WE5U7vhZI7cr5kzn9zL1+sTAt1i/4cxAhiInQgiGDhmEKp0mfb/okHWQk4BxImG+JMh1eVnvwUIBGPm/Q1U+cTE07p3yW7fdTnhhL00WE=&lt;/D&gt;&lt;/RSAKeyValue&gt;";


        private string Uid;


        // Client处理TGT  登录过程 【有uid】
        public Client(string uid)
        {
            this.Uid = uid;
            this.tgTicket = new TGTicket(uid);
            this.kerbAsResponse = asServer.CreateKerbAsResp(uid, out tgTicket);
        }

        // Client处理TGT  登录过程【没有TGT，即没有uid】
        public Client(string userName, string userPwd, out User u)
        {
            this.kerbAsReq = new KerbASRequest(userName, userPwd);
            User user = asServer.UserValidate(kerbAsReq, ref errorInfo, out tgTicket, out kerbAsResponse);
            u = user;
            if (user != null)
            {
                this.Uid = user.UserCode;
            }
            else
            {
                this.Uid = "";
            }

        }


        /**
           还有一种解决对象和字符串的方法就是：  
         * 直接将对象转换成JSON类型  或者直接用：对象的序列化
         *    
         */
        public KerbTGSRequest CreateKerbTGSReq(string ADMIN_UID)   //产生KerbTGSRequest请求
        {


            this.Uid = ADMIN_UID; //这里进行初始化



            string[] strArray0 = kerbAsResponse.Split(',');
            string rsasign = strArray0[1];
            //string decryptKerbAsResp = desCryp.Decrypt(kerbAsResponse, KeyType.AS_Client_Key, KeyType.Iv);  //没有添加数字签名前的解密操作
            string decryptKerbAsResp = desCryp.Decrypt(strArray0[0], KeyType.AS_Client_Key, KeyType.Iv);

            string[] strArray = decryptKerbAsResp.Split('|');
            string encrypttgTicket = "";
            string session_key_1 = "";
            string uid = "";
            encrypttgTicket = strArray[0];
            session_key_1 = strArray[1];
            uid = strArray[2];

            // 数字签名认证的流程：
            //string strhash = rsaCryp.RSAEncrypt(HttpUtility.HtmlDecode(publicKey), session_key_1); //这个与ASServer中产生的key1text不一样
            string hashdata = "";
            rsaCryp.GetHash(session_key_1, ref hashdata);
            if (rsaCryp.SignatureDeformatter(HttpUtility.HtmlDecode(publicKey), hashdata, rsasign))
            {

                // 在Client端不能对TGTicket进行解密处理。
                //string tgticket = desCryp.Decrypt(encrypttgTicket, KeyType.AS_TGS_Key, KeyType.Iv);
                //string[] ticketArray = tgticket.Split('|');
                //string uid = ticketArray[0];
                //DateTime ts2 = Convert.ToDateTime(ticketArray[1]);
                //double lifetime2 = Convert.ToDouble(ticketArray[2]);           

                //this.tgTicket = new TGTicket(uid,ts2,lifetime2);
                //string encryptUid = desCryp.Encrypt(uid, session_key_1, KeyType.Iv);
                //this.kerbTgsReq = new KerbTGSRequest(this.tgTicket,session_key_1,encryptUid);

                string encryptUid = desCryp.Encrypt(uid, session_key_1, KeyType.Iv);
                this.kerbTgsReq = new KerbTGSRequest(encrypttgTicket, session_key_1, encryptUid);                
                return this.kerbTgsReq;
            }




            else  //表示Session_key_1被修改 ，做的只有重新生成密钥。
            {
                errorInfo = "随机密钥发生变化,请重新生成随机密钥。";  //保证密钥的安全性
                this.kerbAsResponse = asServer.CreateKerbAsResp(this.Uid, out this.tgTicket);  // kerbAsResponse发生变化
                this.kerbTgsReq = CreateKerbTGSReq(ADMIN_UID);
                return this.kerbTgsReq;
            }



        }

        public KerbAPRequest CreateKerbApReq()   //产生KerbApRequest请求
        {
            // out 可以不初始化
            bool tgsvalid;
            string kerbTgsResp;
            tgServer.HandleTgsReq(this.kerbTgsReq, out tgsvalid, out kerbTgsResp);

            if (tgsvalid)   //只有在TGS票据合法的时候  uid也是合法TGS中的用户凭证信息
            {
                //string[] strArray0 = kerbTgsResp.Split(',');
                // string rsasign = strArray0[1];
                string[] strArray = kerbTgsResp.Split('|');
                string encryptSticket = strArray[0];
                string client_ap_key = strArray[1];
                string uid1 = strArray[2];

                string hashdata = "";
                rsaCryp.GetHash(client_ap_key, ref hashdata);



                //如果这里的用户非法，则不能解密STicket 也就无法修改
                // 如果在APServer中接到的STicket不合法，只能说明此处用户非法
                //string sticket = desCryp.Decrypt(encryptSticket, KeyType.TGS_AP_Key, KeyType.Iv); 
                //string[] ticketArray = sticket.Split('|');
                //string sIdentity = ticketArray[0];
                //DateTime ts4 = Convert.ToDateTime(ticketArray[1]);
                //double lifetime4 = Convert.ToDouble(ticketArray[2]);
                //string uid2 = ticketArray[3];
                //int adc = Convert.ToInt32(ticketArray[4]);

                //this.sTicket = new STicket(uid2,sIdentity,ts4,adc,lifetime4);
                //this.auth = new Authenticator(uid1, client_ap_key,adc);
                //this.kerbApReq = new KerbAPRequest(this.sTicket, this.auth);
                this.auth = new Authenticator(uid1, client_ap_key);
                this.kerbApReq = new KerbAPRequest(encryptSticket, this.auth);
                return this.kerbApReq;



                


            }



            else
            {
                Console.Write("Heelo");
                tgServer.HandleTgsReq(this.kerbTgsReq, out tgsvalid, out kerbTgsResp);
                return this.kerbApReq;
            }
        }

        // 可以访问网络服务
        public bool VisitApp()
        {
            bool stvalid;
            bool apResponse;
            apServer.HandleApReq(this.kerbApReq, out stvalid, out apResponse);

            if (stvalid)  // 票据合法的话
            {
                if (apResponse)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Authen(string uid)
        {
            // 用户发送TGS请求的条件 ：
            // 【TGTicket存在】  
            if (!string.IsNullOrEmpty(uid))
            {
                this.kerbTgsReq = CreateKerbTGSReq(uid);

                this.kerbApReq = CreateKerbApReq();
                if (VisitApp())  //访问
                {
                    return true;
                }
            }
            return false;
        }

    }
}
