using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.ContactManagement.DTOs;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement
{
    public interface IAddressAndContactAppService:IApplicationService
    {
        Task CreateAAC(CreateAddressAndContactInput input);
        Task DeleteAAC(DeleteAddressAndContactInput input);
        Task<List<int>> GetAddressIds(int contactId);
        Task CleanUpAAC();
    }
}
