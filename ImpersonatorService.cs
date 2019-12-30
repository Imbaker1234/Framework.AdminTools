using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace AdminTools
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("945B8524-2482-42E6-9E1B-68827007D541")]
    [ProgId("Prometric.AdminTools.AdminService")]
    [ComDefaultInterface(typeof(IImpersonatorService))]
    [AutomationProxy(true)]
    public class ImpersonatorService : IImpersonatorService
    {
        public string User { private get; set; }
        public string Pass { private get; set; }
        public string Domain { private get; set; }

        public ImpersonatorService()
        {
        }

        public ImpersonatorService(string user, string pass, string domain = "")
        {
            User = user;
            Pass = pass;
            Domain = domain is "" ? Environment.UserDomainName : domain;
        }

        /// <summary>
        /// Attempt to authorize either using the provided credentials, or if none are provided, a
        /// default to the class level credentials. If the authorization fails due to incorrect
        /// credentials or lack of both class and argument level parameters this will return null
        /// causing the using block's code to execute as the current user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Impersonator TryImpersonate(string user = null, string pass = null, string domain = null)
        {
            try
            {
                return Impersonate(user, pass, domain);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Impersonator Impersonate(string user = null, string pass = null, string domain = null)
        {
            if (WindowsIdentity.GetCurrent().ImpersonationLevel != TokenImpersonationLevel.None) return null;
            if (user is null) user = User ?? throw new ArgumentException("User is neither set at the class level nor was it provided as an argument.");
            if (pass is null) pass = Pass ?? throw new ArgumentException("Pass is neither set at the class level nor was it provided as an argument.");
            if (domain is null) domain = Domain ?? Environment.UserDomainName;

            return new Impersonator(user, pass, domain);
        }
    }
}