using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sso_Core.Message
{
    /// <summary>
    ///  KerbTGSRequest 是Client向TGS服务器发送的请求票据
    ///  KerbTGSRequest票据请求中有：
    ///  TGTicket ，用户与TGS之间的会话密钥，用会话密钥加密过的uid
    ///  
    /// 这里的TGTicket不应该是一个对象，而应该是一个string类型的加密过后的字符串
    /// 因为client用户不能解密加密过的TGTicket对象，
    /// </summary>
    public class KerbTGSRequest
    {
        //private TGTicket _tgticket;
        private string _session_key_1;
        private string _encryptUid;
        private string _encyptgsTicket;

        #region  属性表
        //public TGTicket tgticket
        //{
        //    set { this._tgticket = value; }
        //    get { return this._tgticket; }
        //}

        public string session_key_1
        {
            get { return this._session_key_1; }
            set { this._session_key_1 = value; }
        }

        public string encryptUid
        {
            set{this._encryptUid = value;}
            get { return this._encryptUid; }
        }

        public string encyptgsTicket
        {
            get { return this._encyptgsTicket; }
            set{this._encyptgsTicket = value;}
        }

        #endregion

        //public KerbTGSRequest(TGTicket tgTicket,string Session_Key_1,string EncryptUid)
        public KerbTGSRequest(string EncyptgsTicket, string Session_Key_1, string EncryptUid)
        {
            this._encryptUid = EncryptUid;
            this._session_key_1 = Session_Key_1;
            //this._tgticket = tgTicket;
            this._encyptgsTicket = EncyptgsTicket;
        }
    }
}
