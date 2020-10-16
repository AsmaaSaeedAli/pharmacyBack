using Abp.Authorization;
using Pharmacy.Authorization.Roles;
using Pharmacy.Authorization.Users;

namespace Pharmacy.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
