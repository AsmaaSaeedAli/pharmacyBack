using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Pharmacy.Authorization.Users;
using Pharmacy.MultiTenancy;

namespace Pharmacy.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}