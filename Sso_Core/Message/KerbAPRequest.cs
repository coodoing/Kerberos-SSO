using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sso_Core.Security;

namespace Sso_Core.Message
{
    /// <summary>
    /// KerbAPRequest请求中包含的信息是:
    /// STicket 与uid 以及   用户与Ap应用服务之间的会话密钥
    /// </summary>
    public class KerbAPRequest
    {
        //private STicket _sticket;
        private string _encrptSticket;
        private Authenticator _authenticator;

        #region  属性
        //public STicket sticket
        //{
        //    set { this._sticket = value; }
        //    get { return this._sticket; }
        //}

        public string encrptSticket
        {
            get{return this._encrptSticket;}
            set{this._encrptSticket = value;}
        }

        public Authenticator authenticator
        {
            set { this._authenticator = value; }
            get { return this._authenticator; }
        }
        
        #endregion

        //public KerbAPRequest(STicket sTicket, Authenticator authen)
        public KerbAPRequest(string EncrptSticket, Authenticator authen)
        {
            this._encrptSticket = EncrptSticket;
            this._authenticator = authen;
        }
    }
}
