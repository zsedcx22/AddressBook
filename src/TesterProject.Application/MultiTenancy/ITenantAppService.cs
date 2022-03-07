using Abp.Application.Services;
using TesterProject.MultiTenancy.Dto;

namespace TesterProject.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

