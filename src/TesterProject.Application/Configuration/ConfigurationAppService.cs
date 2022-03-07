using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using TesterProject.Configuration.Dto;

namespace TesterProject.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TesterProjectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
