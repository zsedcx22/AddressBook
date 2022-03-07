using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.CarManagement.Entities;
using TesterProject.ContactManagement.DTOs;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement
{
    public interface IContactAppService:IApplicationService
    {
        Task<List<ContactDto>> GetContacts();
        Task<List<AddressDto>> GetContactAddresses(int contactId);
        Task<List<ContactDto>> GetAddressContacts(int addressId);
        Task<List<PhoneDto>> GetPhones(GetPhonesInput input);
        Task<int> CreateContact(CreateContactInput input);
        Task<int> CreateContactAndAddress(CreateContactAndAddressInput input);
        Task CreatePhone(CreatePhoneInput input);
        Task EditContact(EditContactInput input);
        Task EditAddress(EditAddressInput input);
        Task EditPhone(EditPhoneInput input);
        Task DeleteContact(int contactId);
        Task DeleteAddress(int addressId);
        Task DeletePhone(int phoneId);
        Task DeletePhones(int contactId);
        Task AddAddressToContact(AddAddressToContactInput input);
        Task RemoveAddressFromContact(RemoveAddressFromContactInput input);
        Task GetContact2();
    }
}
