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
    public interface IPhoneAppService:IApplicationService
    {
        Task<List<PhoneDto>> GetPhones(GetPhonesInput input);
        Task CreatePhone(CreatePhoneInput input);
        Task EditPhone(EditPhoneInput input);
        Task DeletePhone(int phoneId);
        Task DeletePhones(int contactId);
    }
}
