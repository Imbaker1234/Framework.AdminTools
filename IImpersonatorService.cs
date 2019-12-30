namespace AdminTools
{
    public interface IImpersonatorService
    {
        /// <summary>
        /// The username of the user associated with this Service.
        /// </summary>
        string User { set; }

        /// <summary>
        /// The password of the user associated with this Service.
        /// </summary>
        string Pass { set; }

        /// <summary>
        /// The domain of the user associated with this Service. This will default to
        /// the domain of the current user if not set.
        /// </summary>
        string Domain { set; }

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
        Impersonator TryImpersonate(string user = null, string pass = null, string domain = null);

        /// <summary>
        /// <para>
        /// Provides an Impersonator to allow the program to act using another users permissions.
        /// </para>
        /// <para>
        /// All parameters are optional, defaulting to the class level variables if set, and
        /// overriding the class level variables if provided.
        /// </para>
        /// <para>
        /// Multiple levels of impersonation are not permitted by this service but can be achieved
        /// by calling for the Impersonator object directly. This avoids wasteful or complex logic
        /// and avoids re-impersonating on recursive calls.
        /// </para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        Impersonator Impersonate(string user = null, string pass = null, string domain = null);
    }
}