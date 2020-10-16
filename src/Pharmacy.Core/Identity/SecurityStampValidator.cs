using Abp.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pharmacy.Authorization.Roles;
using Pharmacy.Authorization.Users;
using Pharmacy.MultiTenancy;

namespace Pharmacy.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            SignInManager signInManager,
            ISystemClock systemClock,
            ILoggerFactory loggerFactory)
            : base(options, signInManager, systemClock, loggerFactory)
        {
        }
    }
}