using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sso_Core.Enumeration
{
    /// <summary>
    /// 登录状态码
    /// </summary>
    public enum LoginStatus
    {
        Success,   //成功

        FirstLogin,   // 成功，但是首次登录

        NotLog,     // 登出 ,没有登录
         
        UserNotExist,  // 用户名不存在

        PasswordWrong,  // 密码错误

        AccountDisable, //账号被停用           

        NoApproval  //账号还未审核(未启用)
    }
}
