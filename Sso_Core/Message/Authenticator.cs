using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sso_Core.Message
{
    /// <summary>
    ///  鉴别码:
    ///  时间戳 uid 以及 会话密钥
    /// </summary>
    public class Authenticator
    {
        private string _uid;
        private DateTime _ts;
        private string _client_ap_key;

        private int _adc=23;  //该值是恒定的

        #region 属性
        public string client_ap_key
        {
            get { return this._client_ap_key; }
            set { this._client_ap_key = value; }
        }

        public string uid
        {
            set { this._uid = value; }
            get { return this._uid; }
        }
        
        public DateTime ts
        {
            get { return this._ts; }
            set{this._ts = value;}
        }
             
        public int adc
        {
            get{return this._adc;}
        }
        #endregion

        //public Authenticator(string Uid, string Client_Ap_Key,int Adc)
        public Authenticator(string Uid, string Client_Ap_Key)
        {
            this._client_ap_key = Client_Ap_Key;
            this._ts = DateTime.Now;
            this._uid = Uid;
        }
    }
}
