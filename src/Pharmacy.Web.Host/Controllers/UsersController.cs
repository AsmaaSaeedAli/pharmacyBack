using Abp.AspNetCore.Mvc.Authorization;
using Pharmacy.Authorization;
using Pharmacy.Storage;
using Abp.BackgroundJobs;

namespace Pharmacy.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}