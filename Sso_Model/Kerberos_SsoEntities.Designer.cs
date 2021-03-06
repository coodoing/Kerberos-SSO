﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

// Original file name:
// Generation date: 2010/5/23 10:41:42
namespace Sso_Model
{
    
    /// <summary>
    /// There are no comments for Kerberos_SsoEntities in the schema.
    /// </summary>
    public partial class Kerberos_SsoEntities : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Initializes a new Kerberos_SsoEntities object using the connection string found in the 'Kerberos_SsoEntities' section of the application configuration file.
        /// </summary>
        public Kerberos_SsoEntities() : 
                base("name=Kerberos_SsoEntities", "Kerberos_SsoEntities")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new Kerberos_SsoEntities object.
        /// </summary>
        public Kerberos_SsoEntities(string connectionString) : 
                base(connectionString, "Kerberos_SsoEntities")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new Kerberos_SsoEntities object.
        /// </summary>
        public Kerberos_SsoEntities(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "Kerberos_SsoEntities")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for LoginLog in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<LoginLog> LoginLog
        {
            get
            {
                if ((this._LoginLog == null))
                {
                    this._LoginLog = base.CreateQuery<LoginLog>("[LoginLog]");
                }
                return this._LoginLog;
            }
        }
        private global::System.Data.Objects.ObjectQuery<LoginLog> _LoginLog;
        /// <summary>
        /// There are no comments for Site in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Site> Site
        {
            get
            {
                if ((this._Site == null))
                {
                    this._Site = base.CreateQuery<Site>("[Site]");
                }
                return this._Site;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Site> _Site;
        /// <summary>
        /// There are no comments for User in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<User> User
        {
            get
            {
                if ((this._User == null))
                {
                    this._User = base.CreateQuery<User>("[User]");
                }
                return this._User;
            }
        }
        private global::System.Data.Objects.ObjectQuery<User> _User;
        /// <summary>
        /// There are no comments for UserSite in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<UserSite> UserSite
        {
            get
            {
                if ((this._UserSite == null))
                {
                    this._UserSite = base.CreateQuery<UserSite>("[UserSite]");
                }
                return this._UserSite;
            }
        }
        private global::System.Data.Objects.ObjectQuery<UserSite> _UserSite;
        /// <summary>
        /// There are no comments for LoginLog in the schema.
        /// </summary>
        public void AddToLoginLog(LoginLog loginLog)
        {
            base.AddObject("LoginLog", loginLog);
        }
        /// <summary>
        /// There are no comments for Site in the schema.
        /// </summary>
        public void AddToSite(Site site)
        {
            base.AddObject("Site", site);
        }
        /// <summary>
        /// There are no comments for User in the schema.
        /// </summary>
        public void AddToUser(User user)
        {
            base.AddObject("User", user);
        }
        /// <summary>
        /// There are no comments for UserSite in the schema.
        /// </summary>
        public void AddToUserSite(UserSite userSite)
        {
            base.AddObject("UserSite", userSite);
        }
    }
    /// <summary>
    /// There are no comments for Kerberos_SsoModel.LoginLog in the schema.
    /// </summary>
    /// <KeyProperties>
    /// LoginLogID
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="Kerberos_SsoModel", Name="LoginLog")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class LoginLog : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new LoginLog object.
        /// </summary>
        /// <param name="loginLogID">Initial value of LoginLogID.</param>
        public static LoginLog CreateLoginLog(int loginLogID)
        {
            LoginLog loginLog = new LoginLog();
            loginLog.LoginLogID = loginLogID;
            return loginLog;
        }
        /// <summary>
        /// There are no comments for Property LoginLogID in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int LoginLogID
        {
            get
            {
                return this._LoginLogID;
            }
            set
            {
                this.OnLoginLogIDChanging(value);
                this.ReportPropertyChanging("LoginLogID");
                this._LoginLogID = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("LoginLogID");
                this.OnLoginLogIDChanged();
            }
        }
        private int _LoginLogID;
        partial void OnLoginLogIDChanging(int value);
        partial void OnLoginLogIDChanged();
        /// <summary>
        /// There are no comments for Property UserCode in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this.OnUserCodeChanging(value);
                this.ReportPropertyChanging("UserCode");
                this._UserCode = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UserCode");
                this.OnUserCodeChanged();
            }
        }
        private string _UserCode;
        partial void OnUserCodeChanging(string value);
        partial void OnUserCodeChanged();
        /// <summary>
        /// There are no comments for Property LoginTime in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] LoginTime
        {
            get
            {
                return global::System.Data.Objects.DataClasses.StructuralObject.GetValidValue(this._LoginTime);
            }
            set
            {
                this.OnLoginTimeChanging(value);
                this.ReportPropertyChanging("LoginTime");
                this._LoginTime = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("LoginTime");
                this.OnLoginTimeChanged();
            }
        }
        private byte[] _LoginTime;
        partial void OnLoginTimeChanging(byte[] value);
        partial void OnLoginTimeChanged();
        /// <summary>
        /// There are no comments for Property LoginIP in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string LoginIP
        {
            get
            {
                return this._LoginIP;
            }
            set
            {
                this.OnLoginIPChanging(value);
                this.ReportPropertyChanging("LoginIP");
                this._LoginIP = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("LoginIP");
                this.OnLoginIPChanged();
            }
        }
        private string _LoginIP;
        partial void OnLoginIPChanging(string value);
        partial void OnLoginIPChanged();
        /// <summary>
        /// There are no comments for Property LoginOS in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string LoginOS
        {
            get
            {
                return this._LoginOS;
            }
            set
            {
                this.OnLoginOSChanging(value);
                this.ReportPropertyChanging("LoginOS");
                this._LoginOS = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("LoginOS");
                this.OnLoginOSChanged();
            }
        }
        private string _LoginOS;
        partial void OnLoginOSChanging(string value);
        partial void OnLoginOSChanged();
    }
    /// <summary>
    /// There are no comments for Kerberos_SsoModel.Site in the schema.
    /// </summary>
    /// <KeyProperties>
    /// SiteID
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="Kerberos_SsoModel", Name="Site")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Site : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Site object.
        /// </summary>
        /// <param name="siteID">Initial value of SiteID.</param>
        /// <param name="siteCode">Initial value of SiteCode.</param>
        public static Site CreateSite(int siteID, string siteCode)
        {
            Site site = new Site();
            site.SiteID = siteID;
            site.SiteCode = siteCode;
            return site;
        }
        /// <summary>
        /// There are no comments for Property SiteID in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int SiteID
        {
            get
            {
                return this._SiteID;
            }
            set
            {
                this.OnSiteIDChanging(value);
                this.ReportPropertyChanging("SiteID");
                this._SiteID = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SiteID");
                this.OnSiteIDChanged();
            }
        }
        private int _SiteID;
        partial void OnSiteIDChanging(int value);
        partial void OnSiteIDChanged();
        /// <summary>
        /// There are no comments for Property SiteUrl in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string SiteUrl
        {
            get
            {
                return this._SiteUrl;
            }
            set
            {
                this.OnSiteUrlChanging(value);
                this.ReportPropertyChanging("SiteUrl");
                this._SiteUrl = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("SiteUrl");
                this.OnSiteUrlChanged();
            }
        }
        private string _SiteUrl;
        partial void OnSiteUrlChanging(string value);
        partial void OnSiteUrlChanged();
        /// <summary>
        /// There are no comments for Property SiteName in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string SiteName
        {
            get
            {
                return this._SiteName;
            }
            set
            {
                this.OnSiteNameChanging(value);
                this.ReportPropertyChanging("SiteName");
                this._SiteName = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("SiteName");
                this.OnSiteNameChanged();
            }
        }
        private string _SiteName;
        partial void OnSiteNameChanging(string value);
        partial void OnSiteNameChanged();
        /// <summary>
        /// There are no comments for Property SiteDescription in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string SiteDescription
        {
            get
            {
                return this._SiteDescription;
            }
            set
            {
                this.OnSiteDescriptionChanging(value);
                this.ReportPropertyChanging("SiteDescription");
                this._SiteDescription = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("SiteDescription");
                this.OnSiteDescriptionChanged();
            }
        }
        private string _SiteDescription;
        partial void OnSiteDescriptionChanging(string value);
        partial void OnSiteDescriptionChanged();
        /// <summary>
        /// There are no comments for Property SiteCode in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string SiteCode
        {
            get
            {
                return this._SiteCode;
            }
            set
            {
                this.OnSiteCodeChanging(value);
                this.ReportPropertyChanging("SiteCode");
                this._SiteCode = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("SiteCode");
                this.OnSiteCodeChanged();
            }
        }
        private string _SiteCode;
        partial void OnSiteCodeChanging(string value);
        partial void OnSiteCodeChanged();
        /// <summary>
        /// There are no comments for Property PublicKey in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string PublicKey
        {
            get
            {
                return this._PublicKey;
            }
            set
            {
                this.OnPublicKeyChanging(value);
                this.ReportPropertyChanging("PublicKey");
                this._PublicKey = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PublicKey");
                this.OnPublicKeyChanged();
            }
        }
        private string _PublicKey;
        partial void OnPublicKeyChanging(string value);
        partial void OnPublicKeyChanged();
        /// <summary>
        /// There are no comments for Property PrivateKey in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string PrivateKey
        {
            get
            {
                return this._PrivateKey;
            }
            set
            {
                this.OnPrivateKeyChanging(value);
                this.ReportPropertyChanging("PrivateKey");
                this._PrivateKey = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrivateKey");
                this.OnPrivateKeyChanged();
            }
        }
        private string _PrivateKey;
        partial void OnPrivateKeyChanging(string value);
        partial void OnPrivateKeyChanged();
    }
    /// <summary>
    /// There are no comments for Kerberos_SsoModel.User in the schema.
    /// </summary>
    /// <KeyProperties>
    /// UserID
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="Kerberos_SsoModel", Name="User")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class User : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new User object.
        /// </summary>
        /// <param name="userID">Initial value of UserID.</param>
        /// <param name="userCode">Initial value of UserCode.</param>
        /// <param name="loginName">Initial value of LoginName.</param>
        /// <param name="password">Initial value of Password.</param>
        public static User CreateUser(int userID, string userCode, string loginName, string password)
        {
            User user = new User();
            user.UserID = userID;
            user.UserCode = userCode;
            user.LoginName = loginName;
            user.Password = password;
            return user;
        }
        /// <summary>
        /// There are no comments for Property UserID in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this.OnUserIDChanging(value);
                this.ReportPropertyChanging("UserID");
                this._UserID = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UserID");
                this.OnUserIDChanged();
            }
        }
        private int _UserID;
        partial void OnUserIDChanging(int value);
        partial void OnUserIDChanged();
        /// <summary>
        /// There are no comments for Property UserCode in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this.OnUserCodeChanging(value);
                this.ReportPropertyChanging("UserCode");
                this._UserCode = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("UserCode");
                this.OnUserCodeChanged();
            }
        }
        private string _UserCode;
        partial void OnUserCodeChanging(string value);
        partial void OnUserCodeChanged();
        /// <summary>
        /// There are no comments for Property LoginName in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string LoginName
        {
            get
            {
                return this._LoginName;
            }
            set
            {
                this.OnLoginNameChanging(value);
                this.ReportPropertyChanging("LoginName");
                this._LoginName = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("LoginName");
                this.OnLoginNameChanged();
            }
        }
        private string _LoginName;
        partial void OnLoginNameChanging(string value);
        partial void OnLoginNameChanged();
        /// <summary>
        /// There are no comments for Property Password in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this.OnPasswordChanging(value);
                this.ReportPropertyChanging("Password");
                this._Password = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Password");
                this.OnPasswordChanged();
            }
        }
        private string _Password;
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
        /// <summary>
        /// There are no comments for Property Email in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this.OnEmailChanging(value);
                this.ReportPropertyChanging("Email");
                this._Email = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Email");
                this.OnEmailChanged();
            }
        }
        private string _Email;
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
        /// <summary>
        /// There are no comments for Property LastLoginIP in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string LastLoginIP
        {
            get
            {
                return this._LastLoginIP;
            }
            set
            {
                this.OnLastLoginIPChanging(value);
                this.ReportPropertyChanging("LastLoginIP");
                this._LastLoginIP = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("LastLoginIP");
                this.OnLastLoginIPChanged();
            }
        }
        private string _LastLoginIP;
        partial void OnLastLoginIPChanging(string value);
        partial void OnLastLoginIPChanged();
        /// <summary>
        /// There are no comments for Property LastLoginOS in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string LastLoginOS
        {
            get
            {
                return this._LastLoginOS;
            }
            set
            {
                this.OnLastLoginOSChanging(value);
                this.ReportPropertyChanging("LastLoginOS");
                this._LastLoginOS = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("LastLoginOS");
                this.OnLastLoginOSChanged();
            }
        }
        private string _LastLoginOS;
        partial void OnLastLoginOSChanging(string value);
        partial void OnLastLoginOSChanged();
        /// <summary>
        /// There are no comments for Property Phone in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Nullable<int> Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                this.OnPhoneChanging(value);
                this.ReportPropertyChanging("Phone");
                this._Phone = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Phone");
                this.OnPhoneChanged();
            }
        }
        private global::System.Nullable<int> _Phone;
        partial void OnPhoneChanging(global::System.Nullable<int> value);
        partial void OnPhoneChanged();
        /// <summary>
        /// There are no comments for Property UserState in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Nullable<int> UserState
        {
            get
            {
                return this._UserState;
            }
            set
            {
                this.OnUserStateChanging(value);
                this.ReportPropertyChanging("UserState");
                this._UserState = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UserState");
                this.OnUserStateChanged();
            }
        }
        private global::System.Nullable<int> _UserState;
        partial void OnUserStateChanging(global::System.Nullable<int> value);
        partial void OnUserStateChanged();
        /// <summary>
        /// There are no comments for Property UserRemark in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string UserRemark
        {
            get
            {
                return this._UserRemark;
            }
            set
            {
                this.OnUserRemarkChanging(value);
                this.ReportPropertyChanging("UserRemark");
                this._UserRemark = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UserRemark");
                this.OnUserRemarkChanged();
            }
        }
        private string _UserRemark;
        partial void OnUserRemarkChanging(string value);
        partial void OnUserRemarkChanged();
        /// <summary>
        /// There are no comments for Property LastLoginTime in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Nullable<global::System.DateTime> LastLoginTime
        {
            get
            {
                return this._LastLoginTime;
            }
            set
            {
                this.OnLastLoginTimeChanging(value);
                this.ReportPropertyChanging("LastLoginTime");
                this._LastLoginTime = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("LastLoginTime");
                this.OnLastLoginTimeChanged();
            }
        }
        private global::System.Nullable<global::System.DateTime> _LastLoginTime;
        partial void OnLastLoginTimeChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnLastLoginTimeChanged();
        /// <summary>
        /// There are no comments for Property LastLogoutTime in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string LastLogoutTime
        {
            get
            {
                return this._LastLogoutTime;
            }
            set
            {
                this.OnLastLogoutTimeChanging(value);
                this.ReportPropertyChanging("LastLogoutTime");
                this._LastLogoutTime = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("LastLogoutTime");
                this.OnLastLogoutTimeChanged();
            }
        }
        private string _LastLogoutTime;
        partial void OnLastLogoutTimeChanging(string value);
        partial void OnLastLogoutTimeChanged();
        /// <summary>
        /// There are no comments for Property IsLogin in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Nullable<bool> IsLogin
        {
            get
            {
                return this._IsLogin;
            }
            set
            {
                this.OnIsLoginChanging(value);
                this.ReportPropertyChanging("IsLogin");
                this._IsLogin = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsLogin");
                this.OnIsLoginChanged();
            }
        }
        private global::System.Nullable<bool> _IsLogin;
        partial void OnIsLoginChanging(global::System.Nullable<bool> value);
        partial void OnIsLoginChanged();
    }
    /// <summary>
    /// There are no comments for Kerberos_SsoModel.UserSite in the schema.
    /// </summary>
    /// <KeyProperties>
    /// UserSiteID
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="Kerberos_SsoModel", Name="UserSite")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class UserSite : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new UserSite object.
        /// </summary>
        /// <param name="userSiteID">Initial value of UserSiteID.</param>
        public static UserSite CreateUserSite(int userSiteID)
        {
            UserSite userSite = new UserSite();
            userSite.UserSiteID = userSiteID;
            return userSite;
        }
        /// <summary>
        /// There are no comments for Property UserSiteID in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int UserSiteID
        {
            get
            {
                return this._UserSiteID;
            }
            set
            {
                this.OnUserSiteIDChanging(value);
                this.ReportPropertyChanging("UserSiteID");
                this._UserSiteID = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UserSiteID");
                this.OnUserSiteIDChanged();
            }
        }
        private int _UserSiteID;
        partial void OnUserSiteIDChanging(int value);
        partial void OnUserSiteIDChanged();
        /// <summary>
        /// There are no comments for Property SiteCode in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string SiteCode
        {
            get
            {
                return this._SiteCode;
            }
            set
            {
                this.OnSiteCodeChanging(value);
                this.ReportPropertyChanging("SiteCode");
                this._SiteCode = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("SiteCode");
                this.OnSiteCodeChanged();
            }
        }
        private string _SiteCode;
        partial void OnSiteCodeChanging(string value);
        partial void OnSiteCodeChanged();
        /// <summary>
        /// There are no comments for Property UserCode in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this.OnUserCodeChanging(value);
                this.ReportPropertyChanging("UserCode");
                this._UserCode = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UserCode");
                this.OnUserCodeChanged();
            }
        }
        private string _UserCode;
        partial void OnUserCodeChanging(string value);
        partial void OnUserCodeChanged();
        /// <summary>
        /// There are no comments for Property UserSiteName in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string UserSiteName
        {
            get
            {
                return this._UserSiteName;
            }
            set
            {
                this.OnUserSiteNameChanging(value);
                this.ReportPropertyChanging("UserSiteName");
                this._UserSiteName = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UserSiteName");
                this.OnUserSiteNameChanged();
            }
        }
        private string _UserSiteName;
        partial void OnUserSiteNameChanging(string value);
        partial void OnUserSiteNameChanged();
        /// <summary>
        /// There are no comments for Property UserSitePassword in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string UserSitePassword
        {
            get
            {
                return this._UserSitePassword;
            }
            set
            {
                this.OnUserSitePasswordChanging(value);
                this.ReportPropertyChanging("UserSitePassword");
                this._UserSitePassword = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UserSitePassword");
                this.OnUserSitePasswordChanged();
            }
        }
        private string _UserSitePassword;
        partial void OnUserSitePasswordChanging(string value);
        partial void OnUserSitePasswordChanged();
    }
}
