using System.Collections.Generic;

namespace Pharmacy.Authorization.Roles.Dto
{
    public class GetRolesInput
    {
        public List<string> Permissions { get; set; }
    }
}
