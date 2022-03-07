using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.ContactManagement.DTOs;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement
{
    public class PhoneAppService : TesterProjectAppServiceBase, IPhoneAppService
    {
        private readonly IRepository<Phone> _phoneRepository;
        public PhoneAppService(IRepository<Phone> phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }
        public async Task CreatePhone(CreatePhoneInput input)
        {
            var tempPhone = new Phone() { PhoneNum = input.PhoneNum, Type = input.Type, ContactId = input.ContactId };
            await _phoneRepository.InsertAsync(tempPhone);
        }

        public async Task<List<PhoneDto>> GetPhones(GetPhonesInput input)
        {
            var query = _phoneRepository.GetAll().Where(e=> e.ContactId == input.ContactId);
            return await query.Select(e => new PhoneDto() { Id = e.Id, Type = e.Type, PhoneNum = e.PhoneNum }).ToListAsync();
        }

        public async Task EditPhone(EditPhoneInput input)
        {
            var editee = await _phoneRepository.GetAsync(input.Id);
            editee.PhoneNum = input.PhoneNum;
            editee.Type = input.Type;
        }

        public async Task DeletePhone(int phoneId)
        {
            await _phoneRepository.DeleteAsync(phoneId);
        }

        public async Task DeletePhones(int contactId) //delete all phones associated with a contact - to be used after a contact is deleted
        {
            var delList = await _phoneRepository.GetAllListAsync(e => e.ContactId == contactId);
            foreach(Phone phone in delList)
            {
                await _phoneRepository.DeleteAsync(phone);
            }
        }
    }
}
