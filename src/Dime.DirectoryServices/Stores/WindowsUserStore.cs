using System.Threading.Tasks;

namespace System.DirectoryServices.AccountManagement
{
    /// <summary>
    /// Represents a data store that retrieves Windows users
    /// </summary>
    public class WindowsUserStore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsUserStore"/> class
        /// </summary>
        public WindowsUserStore()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsUserStore"/> class
        /// </summary>
        /// <param name="userName">The user name to login with</param>
        /// <param name="password">The password</param>
        public WindowsUserStore(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsUserStore"/> class
        /// </summary>
        /// <param name="userName">The user name to login with</param>
        /// <param name="password">The password</param>
        /// <param name="domainName">The domain name</param>
        public WindowsUserStore(string userName, string password, string domainName) : this(userName, password)
        {
            Domain = domainName;
            ContextType = !string.IsNullOrEmpty(domainName) ? ContextType.Domain : ContextType.Machine;
        }
        public ContextType ContextType { get; }
        public string UserName { get; }
        public string Password { get; }
        public string Domain { get; }

        /// <summary>
        /// Finds the SID from the user name
        /// </summary>
        /// <param name="loginName">The user name</param>
        /// <returns>The SID</returns>
        public async Task<string> GetSidByLoginName(string loginName)
            => await Task.Run(() =>
            {
                using PrincipalContext context = CreateContext();
                UserPrincipal user = new UserPrincipal(context) { SamAccountName = loginName };
                using PrincipalSearcher searcher = new PrincipalSearcher(user);
                searcher.QueryFilter = user;
                Principal adUser = searcher.FindOne();

                return adUser?.Sid.ToString() ?? string.Empty;
            }).ConfigureAwait(false);

        /// <summary>
        /// Finds the user by SID
        /// </summary>
        /// <param name="sId">The identity's SID</param>
        /// <returns>The logon name</returns>
        /// <remarks>
        /// TODO: Don't swallow exception but let calling method handle it.
        /// Probable reason why it hasn't happen yet is the way how this method is called (~> await Task.WhenAll inside a LINQ statement)
        /// </remarks>
        public string GetLoginNameBySid(string sId)
        {
            try
            {
                if (string.IsNullOrEmpty(sId))
                    return null;

                using PrincipalContext context = CreateContext();
                using UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.Sid, sId);
                DirectoryEntry de = user?.GetUnderlyingObject() as DirectoryEntry;
                return (string)de?.Properties["samAccountName"].Value;
            }
            catch (Exception)
            {
                // Swallow exception
                return null;
            }
        }

        /// <summary>
        /// Shorthand method for creating the PrincipalContext
        /// </summary>
        /// <returns>An instantiated and connected PrincipalContext</returns>
        protected virtual PrincipalContext CreateContext()
            => new PrincipalContext(ContextType, Domain, UserName, Password);
    }
}