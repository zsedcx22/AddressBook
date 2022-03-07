using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement
{
    public class ContactManager : DomainService, IContactManager
    {
        private readonly IRepository<AddressAndContact> _addressAndContactRepository;
        public ContactManager(IRepository<AddressAndContact> addressAndContactRepository)
        {
            _addressAndContactRepository = addressAndContactRepository;
        }

        public async Task AssignContactAddressAsync(int addressId, int contactId) //creates a link between an address and a contact
        {
            if (await _addressAndContactRepository.CountAsync(e => e.AddressID == addressId && e.ContactID == contactId) != 0)
                throw new UserFriendlyException("Association already exists!");

            await _addressAndContactRepository.InsertAsync(new AddressAndContact(addressId, contactId));
        }

        public async Task DeleteAddressAndContacts(int contactId)
        {
            var temp = await _addressAndContactRepository.GetAll().Where(e => e.ContactID == contactId).ToListAsync();

            foreach (AddressAndContact addressAndContact in temp)
                await _addressAndContactRepository.DeleteAsync(addressAndContact);

            /*
                        if (temp.Count == 0)
                        {
                            var contactList = await _addressAndContactRepository.GetAll()
                            .Where(e => e.AddressID == addressId).ToListAsync();
                        }*/
        }

        public async Task RemoveContactAddressAsync(int addressId, int contactId) //remove a link between a contact and an address and then check whether an address has any more associated contacts
        {
            var temp = await _addressAndContactRepository.GetAll().Where(e => e.ContactID == contactId && e.AddressID == addressId).ToListAsync();

            foreach (AddressAndContact addressAndContact in temp)
                await _addressAndContactRepository.DeleteAsync(addressAndContact);
        }

        public async Task<int> CountAddressAndContact(int addressId)
        {
            var contactList = await _addressAndContactRepository.GetAll()
                .Where(e => e.AddressID == addressId).ToListAsync();

            if (contactList == null)
                return 0;
            else
                return contactList.Count();
        }
    }
}
