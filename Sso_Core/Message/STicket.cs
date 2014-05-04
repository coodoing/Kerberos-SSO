using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sso_Core.Message
{
    /// <summary>
    ///  服务访问票据
    /// </summary>
    public class STicket
    {

        private string _stIdentity;   // STicket中的ST票据标志
        private DateTime _ts4;   //
        private double  _lifetime4=10.0;
        private string _uid;

        private int _adc1=0;  // STicket验证标志

        #region  属性
        public string STIdentity
        {
            set { this._stIdentity = value; }
            get { return this._stIdentity; }
        }
        public DateTime TS4
        {
            set { this._ts4 = value; }
            get { return this._ts4; }
        }
        public double LifeTime4
        {
            get { return this._lifetime4; }
        }

        public string Uid
        {
            set { this._uid = value; }
            get { return this._uid; }
        }

        public int Adc1
        {
            get { return this._adc1; }
            set{this._adc1 = value;}
        }
        #endregion

        public STicket(string uId)
        {
            this._uid = uId;
            this._stIdentity = Guid.NewGuid().ToString();
            this._ts4 = DateTime.Now;
            this._adc1 = 23;
        }

        public STicket(string uId,string stIdentity,DateTime time,int adc,double lifeTime)
        {
            this._uid = uId;
            this._stIdentity = stIdentity;
            this._ts4 = time;
            this._adc1 = adc;
            this._lifetime4 = lifeTime;
        }
    }
}
