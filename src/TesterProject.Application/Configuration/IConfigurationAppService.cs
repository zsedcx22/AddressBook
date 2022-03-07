using System.Threading.Tasks;
using TesterProject.Configuration.Dto;

namespace TesterProject.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
