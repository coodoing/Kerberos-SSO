using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sso_Core.Provider.Base
{
    public partial class SsoProvider : ProviderBase
    {
        public static SsoProvider ssoProvider = null;

        public static SsoProvider Instance()
        {
            if (ssoProvider == null)
            {
                ssoProvider = new SsoProvider();
            }
            return ssoProvider;
        }
        

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
