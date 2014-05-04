using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Sso_Core.Helper
{
    public class ConfigHelper
    {
        /// <summary>
        /// 获得配置
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        private static Configuration GetConfiguration(string configName)
        {
                ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                file.ExeConfigFilename = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + configName;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                return config;
        }

        public static Configuration GetGlobalConfig()
        {
            return GetConfiguration("SsoSettings.config");
        }
    }
}
