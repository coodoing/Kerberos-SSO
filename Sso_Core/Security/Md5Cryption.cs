using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Web.Security;

namespace Sso_Core.Security
{
    public class Md5Cryption
    {
        /// <summary>
        /// 使用hash摘要算法对字符串进行单向编码
        /// </summary>
        /// <param name="SourceData">原始字符串</param>
        /// <param name="EncryptType">编码格式：md5、</param>
        /// <returns>编码后的字符串</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1807:AvoidUnnecessaryStringCreation", MessageId = "EncryptType")]
        public static string HashEncrypt(string SourceData, string EncryptType)
        {
            string EncryptData = SourceData;

            switch (EncryptType.ToLower())
            {
                case "md5":
                    EncryptData = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(SourceData, "md5");
                    break;
                default:
                    EncryptData = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(SourceData, EncryptType);
                    break;
            }

            return EncryptData;
        }
    }
}
