using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.CarManagement.DTOs;
using TesterProject.CarManagement.Entities;
using TesterProject.ContactManagement.DTOs;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement
{
    public class ContactAppService : TesterProjectAppServiceBase, IContactAppService
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<AddressAndContact> _addressAndContactRepository;
        private readonly IContactManager _contactManager;
        private readonly IRepository<Phone> _phoneRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public ContactAppService(IRepository<Contact> contactRepository, IRepository<Address> addressRepository, IRepository<AddressAndContact> addressAndContactRepository, 
                                    IContactManager contactManager, IRepository<Phone> phoneRepository, IUnitOfWorkManager unitOfWorkManager) 
        {
            _contactRepository = contactRepository;
            _addressRepository = addressRepository;
            _addressAndContactRepository = addressAndContactRepository;
            _contactManager = contactManager;
            _phoneRepository = phoneRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        //put the info for the linking tables into a list and shove it into the icollection -- potentially need to creatre the linking table in this task based on list of addresses

        public async Task<int> CreateContact(CreateContactInput input) //Create contact based on First Name, Last Name and Date of Birth, without Entities returning the id
        {   
            return await _contactRepository.InsertAndGetIdAsync(new Contact()
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                DateOfBirth = input.DateOfBirth
            });
        }

        public async Task<int> CreateContactAndAddress(CreateContactAndAddressInput input) //Create contact based on First Name, Last Name, Date of Birth and address, returning the id
        {   
            var contactId = await CreateContact(new CreateContactInput() 
            { 
                FirstName = input.FirstName, 
                LastName = input.LastName, 
                DateOfBirth = input.DateOfBirth 
            });
            int addressId;
            var addressList = await _addressRepository.GetAll().Where(e => e.AddressString == input.AddressLine).ToListAsync();
            if (addressList.Count() != 0)
            {
                addressId = addressList[0].Id; 
            }
            else
            {
                addressId = await _addressRepository.InsertAndGetIdAsync(new Address()
                {
                    AddressString = input.AddressLine
                });
            }
            await _contactManager.AssignContactAddressAsync(addressId, contactId);
            return contactId;
        }

        public async Task AddAddressToContact(AddAddressToContactInput input) //Create an address and create a link with a contact based on a contact id
        {
            var addressId = await _addressRepository.InsertAndGetIdAsync(new Address()
            {
                AddressString = input.AddressLine
            });
            await _contactManager.AssignContactAddressAsync(addressId, input.ContactId);
        }

        public async Task CreatePhone(CreatePhoneInput input)
        {
            var tempPhone = new Phone() { PhoneNum = input.PhoneNum, Type = input.Type, ContactId = input.ContactId };
            await _phoneRepository.InsertAsync(tempPhone);
        } //create a new phone

        public async Task<List<PhoneDto>> GetPhones(GetPhonesInput input)
        {
            var query = _phoneRepository.GetAll().Where(e => e.ContactId == input.ContactId);
            return await query.Select(e => new PhoneDto() { Id = e.Id, Type = e.Type, PhoneNum = e.PhoneNum }).ToListAsync();
        } //get all phone numbers associated with a contact

        public async Task<List<ContactDto>> GetContacts() //Get all contacts from the database and return them as a list
        {
            var output = _contactRepository.GetAll().Select(e => new ContactDto()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
            });
            return await output.ToListAsync();


        }

        public async Task<List<AddressDto>> GetContactAddresses(int contactId) //this gets addresses based on a contact id
        {
            return await _addressAndContactRepository.GetAll()
                .Where(e => e.ContactID == contactId)
                .Select(e => new AddressDto() 
                { 
                    Id = e.Address.Id, 
                    AddressString = e.Address.AddressString 
                }).ToListAsync();
        }

        public async Task<List<ContactDto>> GetAddressContacts(int addressId) //Get list of contacts based on an associated address
        {
            return await _addressAndContactRepository.GetAll()
                .Where(e => e.AddressID == addressId)
                .Select(e => new ContactDto()
                {
                    Id = e.Contact.Id,
                    FirstName = e.Contact.FirstName,
                    LastName = e.Contact.LastName,
                    DateOfBirth = e.Contact.DateOfBirth
                }).ToListAsync();
        }

        public async Task EditContact(EditContactInput input) //edit a contact 
        {
            var edit = await _contactRepository.GetAsync(input.Id);
            if (edit.FirstName != input.FirstName)
            {
                if (input.FirstName != null)
                {
                    edit.FirstName = input.FirstName;
                }
                else
                {
                    throw new ArgumentNullException("First Name should not be null!");
                }
            }

            if (edit.LastName != input.LastName)
            {
                if (input.LastName != null)
                {
                    edit.LastName = input.LastName;
                }
                else 
                {
                    throw new ArgumentNullException("Last Name should not be null!");
                }
            }

            if (edit.DateOfBirth != input.DateOfBirth)
            {
                edit.DateOfBirth = input.DateOfBirth;
            }
        }

        public async Task EditAddress(EditAddressInput input) //edit an address
        {
            var edit = await _addressRepository.GetAsync(input.Id);
            if (edit.AddressString != input.AddressString)
            {
                if (input.AddressString != null)
                {
                    edit.AddressString = input.AddressString;
                }
                else
                {
                    throw new ArgumentNullException("Address String should not be null!");
                }
            }
        }

        public async Task EditPhone(EditPhoneInput input) //edit a phone
        {
            var edit = await _phoneRepository.GetAsync(input.Id);
            if (edit.PhoneNum!=input.PhoneNum) 
            {
                if (input.PhoneNum != null)
                {
                    edit.PhoneNum = input.PhoneNum;
                }
                else
                {
                    throw new ArgumentNullException("Phone Number should not be null!");
                }
            }
            if (edit.Type != input.Type)
            {
                if (input.Type != null)
                {
                    edit.Type = input.Type;
                }
                else
                {
                    throw new ArgumentNullException("Phone Type should not be null!");
                }
            }
        }

        public async Task DeleteContact(int contactId) //Delete a contact based on its ID
        {
            //await _contactManager.DeleteAddressAndContacts(contactId);
            await _contactRepository.DeleteAsync(contactId);
        }

        public async Task DeleteAddress(int addressId) //Delete an address based on its ID
        {
            await _addressRepository.DeleteAsync(addressId);
        }

        public async Task RemoveAddressFromContact(RemoveAddressFromContactInput input) //Deletes the linking table between an address and a contact, then if no other links involving that address exists
                                                                                        //then also delete that address
        {
            await _contactManager.RemoveContactAddressAsync(input.AddressId, input.ContactId);
            var count = await _contactManager.CountAddressAndContact(input.AddressId);
            if (count == 0)
                await DeleteAddress(input.AddressId);
        }

        public async Task DeletePhone(int phoneId) //Delete a phone based on its ID
        {
            await _phoneRepository.DeleteAsync(phoneId);
        }
        
        public async Task DeletePhones(int contactId) //delete all phones associated with a contact - to be used after a contact is deleted
        {
            var delList = await _phoneRepository.GetAllListAsync(e => e.ContactId == contactId);
            foreach (Phone phone in delList)
            {
                await _phoneRepository.DeleteAsync(phone);
            }
        }


        public async Task GetContact2()
        {
            //var x = from con in await _contactRepository.GetAllListAsync() where con.Id == 2 select con.FirstName;
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = _contactRepository.GetAll().AsNoTracking().Where(e => e.Id == 5).Select(e => e.FirstName);
                var firstName = await query.SingleOrDefaultAsync();
            }
        } //for testing purposes
    }
}
