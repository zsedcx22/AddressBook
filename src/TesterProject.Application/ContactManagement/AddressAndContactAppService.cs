using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.ContactManagement.DTOs;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement
{
    public class AddressAndContactAppService : TesterProjectAppServiceBase, IAddressAndContactAppService
    {
        private readonly IRepository<AddressAndContact> _addressAndContactRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Contact> _contactRepository;
        public AddressAndContactAppService(IRepository<AddressAndContact> addressAndContactRepository, IRepository<Address> addressRepository, IRepository<Contact> contactRepository)
        {
            _addressAndContactRepository = addressAndContactRepository;
            _addressRepository = addressRepository;
            _contactRepository = contactRepository;
        }

        public async Task CreateAAC(CreateAddressAndContactInput input)
        {
            //_addressAndContactRepository.
            var checkList = await _addressAndContactRepository.GetAllListAsync(e => e.AddressID == input.AddressId && e.ContactID == input.ContactId);
            //await _addressAndContactRepository.InsertAsync(new AddressAndContact() { Address = input.Address, Contact= input.Contact, AddressID = input.Address.Id, ContactID = input.Contact.Id });
            if (checkList.Count() == 0) //should maybe change to an if count<1 instead
            {
                var tempAAC = await _addressAndContactRepository.InsertAsync(new AddressAndContact());
                tempAAC.Address = await _addressRepository.GetAsync(input.AddressId);
                tempAAC.Contact = await _contactRepository.GetAsync(input.ContactId);
                tempAAC.AddressID = input.AddressId;
                tempAAC.ContactID = input.ContactId;
                
                var tempAddress = await _addressRepository.GetAsync(input.AddressId);
                if (tempAddress.AddressAndContacts == null)
                {
                    tempAddress.AddressAndContacts = new List<AddressAndContact>();
                }
                else
                {
                    tempAddress.AddressAndContacts.Add(tempAAC);
                }

                var tempContact = await _contactRepository.GetAsync(input.ContactId);
                if (tempContact.AddressAndContacts == null)
                {
                    tempContact.AddressAndContacts = new List<AddressAndContact>();
                }
                else
                {
                    tempContact.AddressAndContacts.Add(tempAAC);
                }
                tempContact.AddressAndContacts.Add(tempAAC);
            }
            else
            {
                Console.WriteLine("Link element already exists.");
            }
            //await _addressAndContactRepository.UpdateAsync(new AddressAndContact() { Address = input.Address, Contact = input.Contact, AddressID = input.Address.Id, ContactID = input.Contact.Id });
        }

        public async Task DeleteAAC(DeleteAddressAndContactInput input)
        {
            var deleteList = await _addressAndContactRepository.GetAllListAsync(e => e.AddressID == input.AddressId && e.ContactID == input.ContactId);
            foreach (AddressAndContact del in deleteList)
            {
                await _addressAndContactRepository.DeleteAsync(del);
            }
        }

        public async Task<List<int>> GetAddressIds(int contactId)
        {
            var aacList = await _addressAndContactRepository.GetAllListAsync(e => e.ContactID == contactId);
            var idList = new List<int>();
            foreach (AddressAndContact aac in aacList)
            {
                idList.Add(aac.AddressID);
            }
            return idList;
        }

        public async Task<List<int>> GetContactIds(int addressId)
        {
            var aacList = await _addressAndContactRepository.GetAllListAsync(e => e.AddressID == addressId);
            var idList = new List<int>();
            foreach (AddressAndContact aac in aacList)
            {
                idList.Add(aac.ContactID);
            }
            return idList;
        }

        public async Task CleanUpAAC() 
        {
            var all = await _addressAndContactRepository.GetAllListAsync(); //can change this to update prob
            foreach (AddressAndContact x in all)
            {
                var tempCheck = await _addressAndContactRepository.GetAllListAsync(e => e.Contact == x.Contact && e.Address == x.Address);
                if (tempCheck.Count() > 1)
                {
                    tempCheck.Remove(x);
                    foreach(AddressAndContact y in tempCheck)
                    {
                        await _addressAndContactRepository.DeleteAsync(y);
                    }
                }
            }
        }

    }
}
