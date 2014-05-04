using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Sso_Core.Security;

namespace Sso_Core.Message
{
    /// <summary>
    ///  用户向KDC发出AS认证请求
    ///             依据是书中的Kerberos流程    /// 
    /// KerbASRequest请求数据包中有：用户名，用户身份识别信息以及时间标记
    /// 
    /// 
    /// 
    /// 
    /// KerbAsRequest 请求的时效是在整个session期间  目的是session存在的话，请求就有效，若session消失，则必须重新提交请求
    /// </summary>
    public class KerbASRequest
    {
        private string _userName;
        private string _userPwd;
        private DateTime _kerbAsReqTime;  // 时间标志

        public RSACryption rsaCry = new RSACryption();
        public DESCryption desCry = new DESCryption();            

        public KerbASRequest()
        {
        }

        public KerbASRequest(string userName,string userPwd)
        {
            this._kerbAsReqTime = DateTime.Now;
            this._userName = userName;
            this._userPwd = userPwd;
        }

        #region  Value属性值
        public string UserName
        {
            set
            {
                _userName = value;
            }
            get
            {
                return _userName;
            }
        }

        public string UserPwd
        {
            set
            {
                _userPwd = value;
            }
            get
            {
                return _userPwd;
            }
        }

        //public string UserCode
        //{
        //    set
        //    {
        //        _userCode = value;
        //    }
        //    get
        //    {
        //        return _userCode;
        //    }
        //}
        //public string Identity
        //{
        //    get
        //    {
        //        return _identity;
        //    }
        //    set
        //    {
        //        if (_identity == value)
        //            return;
        //        _identity = value;
        //    }
        //}
        public DateTime KerbAsReqTime
        {
            set
            {
                _kerbAsReqTime = value;
            }
            get
            {
                return _kerbAsReqTime;
            }
        }

        #endregion

        #region
        /// <summary>
        /// 获取用户口令的散列值：长期密钥
        /// </summary>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public string GetUserSecretKey(string userPwd)
        {
            string hashKey = string.Empty;
            bool result = false;
            result = rsaCry.GetHash(userPwd, ref hashKey);
            return hashKey;
        }

        public void SendASRequest()
        {
            string key = GetUserSecretKey(_userPwd);
            string iv = "";
            desCry.GenerateDesCryProvider(key, out iv);
            string privateInfo = Convert.ToString(_kerbAsReqTime);        // 隐蔽的信息： 如用户标识，时间戳
            string encryptReq = desCry.Encrypt(privateInfo, key, iv);  //加密处理后的结果
            string requestStr = _userName + encryptReq;
        }
        #endregion
    }
}
