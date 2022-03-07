using System.Threading.Tasks;
using Abp.Application.Services;
using TesterProject.Authorization.Accounts.Dto;

namespace TesterProject.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
