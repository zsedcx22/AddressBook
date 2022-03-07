using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace TesterProject.Controllers
{
    public abstract class TesterProjectControllerBase: AbpController
    {
        protected TesterProjectControllerBase()
        {
            LocalizationSourceName = TesterProjectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
