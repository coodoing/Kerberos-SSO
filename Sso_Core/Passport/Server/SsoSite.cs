using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Sso_Core.Interface.Server;

namespace Sso_Core.Passport.Server
{
    public class SsoSite : ISite
    {
        private string _SiteID = "";
        private string _PublicKey = "&lt;RSAKeyValue&gt;&lt;Modulus&gt;wYCBD0XgHQ6xUZguY4kcYlR5d5BJGu9RkXeGp+1Y1DGOL46rCQ1LkhJjPPTNOS7wchETKsGoutaojoI74Al/p6ZIRDD8b6SDaOBE1X0BFFle5DALtaTF7LblsP3p6TcTOJNb7LjbSyJrpiAtf5zREbGzLJfVRITyictXFOrRpLs=&lt;/Modulus&gt;&lt;Exponent&gt;AQAB&lt;/Exponent&gt;&lt;/RSAKeyValue&gt;";
        private string _PrivateKey = "&lt;RSAKeyValue&gt;&lt;Modulus&gt;wYCBD0XgHQ6xUZguY4kcYlR5d5BJGu9RkXeGp+1Y1DGOL46rCQ1LkhJjPPTNOS7wchETKsGoutaojoI74Al/p6ZIRDD8b6SDaOBE1X0BFFle5DALtaTF7LblsP3p6TcTOJNb7LjbSyJrpiAtf5zREbGzLJfVRITyictXFOrRpLs=&lt;/Modulus&gt;&lt;Exponent&gt;AQAB&lt;/Exponent&gt;&lt;P&gt;9SAz77XW+TJ7sSX5fsOH7hna48rND1qdl4qxgRIm42j+JufXmke2m5kXD33XAD2dhga8irqi4RRPGag19UhZDw==&lt;/P&gt;&lt;Q&gt;yhYHorGI/g/jtiyl75PWgFvumzUWAyfn4zxLYIrYx4rwDkW0y4zI0f+rn4NAEA5yMG6HGf5INOdGP6SGK1dBlQ==&lt;/Q&gt;&lt;DP&gt;x9PSww2oDEo9T7K3a8GWpGHrcVu4Q1YJtqpX/fKARU8oMSs7NghUMxBgHj7l8MpKCiKfvTBc68QUn8PjCOxLvQ==&lt;/DP&gt;&lt;DQ&gt;w5RskV2nCtPP+2jcs7Bt4c6Xb+kBw84kU7zv6qCWSxDBYd6+ql03ol4B+KArKR8CDrN514NM2L6YM1IEc/+/vQ==&lt;/DQ&gt;&lt;InverseQ&gt;yLGIg+mZO6tzp2dBOBastCFIe59QiU7Flo0kCes+iBglt0smP2QHM87GR/Hwtmrww6sFrEELpe0QNXLMUGx9rw==&lt;/InverseQ&gt;&lt;D&gt;D2mQ3gqOw0G8VedtKnhtTle0JRXU/NQ7Bv8iNpXtEVmxHZCbhukML+JPDTficL+QX0WE5U7vhZI7cr5kzn9zL1+sTAt1i/4cxAhiInQgiGDhmEKp0mfb/okHWQk4BxImG+JMh1eVnvwUIBGPm/Q1U+cTE07p3yW7fdTnhhL00WE=&lt;/D&gt;&lt;/RSAKeyValue&gt;";
        private string _UidKey= "uid";
        private string _FromUrlKey = "fromurl";
        private string _HomePage = "";        
        private string _LogOutUrl = "";
        private int _TimeOut = 23;

        public SsoSite()
        {

        }

        public SsoSite(string siteId)
        {
            this._SiteID = siteId;
            // Server对象的Html与URL编码：
            // Html编码：在最终的HTML页面中把“ <” 变成 “&lt;”
            // URL编码 : 把字符串转换为可疑在URL中使用的格式，转义空格及其他特殊字符
            this._PublicKey = HttpUtility.HtmlDecode(_PublicKey);
            this._PrivateKey = HttpUtility.HtmlDecode(_PrivateKey);
        }


        #region ISite Members

        public string SiteID
        {
            get
            {
                return this._SiteID;
            }
            set
            {
                _SiteID = value;
            }
        }

        public string HomePage
        {
            get
            {
                return _HomePage;
            }
            set
            {
                _HomePage = value;
            }
        }

        public string LogOutUrl
        {
            get
            {
                return _LogOutUrl;
            }
            set
            {
                _LogOutUrl = value;
            }
        }

        public string PublicKey
        {
            get
            {
                return _PublicKey;
            }
            set
            {
                _PublicKey = value;
            }
        }

        public string PrivateKey
        {
            get
            {
                return _PrivateKey;
            }
            set
            {
                _PrivateKey = value;
            }
        }

        public string FromUrlKey
        {
            get
            {
                return _FromUrlKey;
            }
            set
            {
                _FromUrlKey = value;
            }
        }

        public string UidKey
        {
            get
            {
                return _UidKey;
            }
            set
            {
                _UidKey = value;
            }
        }

        public void Add()
        {

        }

        #endregion
    }
}
