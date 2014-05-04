using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Sso_Core.Helper
{
    public class SsoSettingsHelper
    {
        public static SsoSettingsHelper ssoSetting = null;
        public static SsoSettingsHelper Instance()
        {
            if (ssoSetting == null)
            {
                ssoSetting = new SsoSettingsHelper();
            }
            return ssoSetting;
        }

        public SsoSettingsHelper()
        {
            Configuration configHelper = ConfigHelper.GetGlobalConfig();
            m_LoginInUrl = configHelper.AppSettings.Settings["LoginInUrl"].Value.Trim();
            m_LogOutUrl = configHelper.AppSettings.Settings["LogOutUrl"].Value.Trim();
            m_PublicKey = configHelper.AppSettings.Settings["PublicKey"].Value.Trim();
            m_PrivateKey = configHelper.AppSettings.Settings["PrivateKey"].Value.Trim();
            m_SiteCode = configHelper.AppSettings.Settings["SiteCode"].Value.Trim();
            /*
             * int.TryParse() 使用方法： 将其他类型转换为int型 
             转换成功返回 true，转换失败返回 false。最后一个参数为输出值，如果转换失败，输出值为 0
             */
            bool result = int.TryParse(configHelper.AppSettings.Settings["TimeOut"].Value.Trim(), out m_TimeOut);
            if (!result)
            {
                LogHelper.Error("SsoSettings.config中TimeOut配置错误。");
            }
        }
        #region 
        private string m_LoginInUrl = string.Empty;
        public string LoginInUrl
        {
            get { return m_LoginInUrl; }
        }
        private string m_LogOutUrl = string.Empty;
        public string LogOutUrl
        {
            get { return m_LogOutUrl; }
        }
        private int m_TimeOut = 23;
        public int TimeOut
        {
            get { return m_TimeOut; }
        }
        private string m_PublicKey = string.Empty;
        public string PublicKey
        {
            get { return m_PublicKey; }
        }
        private string m_PrivateKey = string.Empty;
        public string PrivateKey
        {
            get { return m_PrivateKey; }
        }
        private string m_SiteCode = string.Empty;
        public string SiteCode
        {
            get { return m_SiteCode; }
        }        
        #endregion
    }
}
