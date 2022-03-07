using System.Threading.Tasks;
using Abp.Application.Services;
using TesterProject.Sessions.Dto;

namespace TesterProject.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
