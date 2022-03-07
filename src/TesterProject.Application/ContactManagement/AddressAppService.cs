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
    public class AddressAppService : TesterProjectAppServiceBase, IAddressAppService
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<AddressAndContact> _addressAndContactRepository;
        public AddressAppService(IRepository<Address> addressRepository, IRepository<AddressAndContact> addressAndContactRepository)
        {
            _addressRepository = addressRepository;
            _addressAndContactRepository = addressAndContactRepository;
        }

        public async Task<int> CreateAddress(CreateAddressInput input) //have separate tasks to check if address already exists --maybe not, could have it check before trying to insert <--check documentation
        {
            //await _contactRepository.InsertAsync(new Contact() { Id = input.Id, FName = input.FName, LName = input.LName, DateOfBirth = input.DateOfBirth, Active = true, EntryDate = DateTime.Now, Cars = cars, Phones = phones, AddressAndContacts = addressAndContacts });
            //CheckAddress();
            var check = await _addressRepository.GetAllListAsync();
            var boolean = check.Exists(e => e.AddressString == input.AddressString);
            if (boolean == false)
            {
                var address = await _addressRepository.InsertAsync(new Address() 
                //can now return id from here if I want to -- will need to include a joiner statement in contact if I don't have a separate function
                {
                    AddressString = input.AddressString,
                    //addressandcontact being created in a separate appservice
                });
                return address.Id;
            }
            else
            {
                var address = await _addressRepository.FirstOrDefaultAsync(e => e.AddressString == input.AddressString);
                return address.Id;
            }
        }

        public async Task EditAddress(EditAddressInput input)//for editing the addressstring only
        {
            var edit = await _addressRepository.UpdateAsync(await _addressRepository.GetAsync(input.Id));
            edit.AddressString = input.AddressString;
        }

        public async Task<List<AddressDto>> GetContactAddresses(GetContactAddressesInput input) //Get list of addresses based on an associated contact
        {
            return await _addressAndContactRepository.GetAll()
                .Where(e => e.ContactID == input.ContactId)
                .Select(e=> new AddressDto() 
                {
                    Id = e.Address.Id, 
                    AddressString = e.Address.AddressString 
                }).ToListAsync();
        }

        public async Task DeleteAddress(int addressId)
        {
            await _addressRepository.DeleteAsync(addressId);
        }
    }
}
