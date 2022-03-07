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
    public interface IAddressAppService:IApplicationService
    {
        Task<List<AddressDto>> GetContactAddresses(GetContactAddressesInput input); //unsure if need the linking table passed in
        Task<int> CreateAddress(CreateAddressInput input); //unsure if need to include to associated contact
        Task EditAddress(EditAddressInput input);
        Task DeleteAddress(int addressId);
    }
}
