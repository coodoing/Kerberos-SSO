using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sso_Core.Helper;

namespace Sso_Core.Message
{
    public class TGTicket
    {
        private string uid;
        private DateTime ts2;  //AS向Client返回TGT的时间
        private double lifetime2=23.0; //=SsoSettingsHelper.Instance().TimeOut;   

        #region  属性
        public string Uid
        {
            set{this.uid = value;}
            get { return this.uid; }
        }
        public DateTime TS2
        {
            set { this.ts2 =value; }
            get { return this.ts2; }
        }
        public double LifeTime2
        {
            get { return this.lifetime2; }
        }
        #endregion

        public TGTicket(string uId)
        {
            this.ts2 = DateTime.Now;
            this.uid = uId;
        }

        public TGTicket(string uId,DateTime ts,double lifetime)
        {
            this.uid = uId;
            this.ts2 = ts;
            this.lifetime2 = lifetime;
        }

    }
}
