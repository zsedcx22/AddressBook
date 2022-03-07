using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.ContactManagement;
using TesterProject.ContactManagement.DTOs;
using TesterProject.ContactManagement.Entities;
using TesterProject.EntityFrameworkCore;
using Xunit;

namespace TesterProject.Tests.AddressBook
{
    public class AddressBook_Tests : TesterProjectTestBase, IClassFixture<SharedDatabaseFixture>
    {
        private readonly IContactAppService _contactAppService;
        public SharedDatabaseFixture Fixture { get; }
        public AddressBook_Tests(SharedDatabaseFixture fixture)
        {
            _contactAppService = Resolve<IContactAppService>();
            Fixture = fixture;
        }

        [Fact]
        public async Task SharedDatabase_Test()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                /*using (var context = Fixture.CreateContext(transaction))
                {
                    var contact = new Contact()
                    {
                        FirstName = "John",
                        LastName = "Frill",
                        DateOfBirth = new DateTime(2001 - 05 - 01),
                        Phones = new List<Phone>() { new Phone() { PhoneNum = "08842995883", Type = "Mobile" } }
                    };

                    var address = new Address()
                    {
                        AddressString = "34 Fragile Lane"
                    };

                    var addressAndContact = new AddressAndContact()
                    {
                        Address = address,
                        AddressID = address.Id,
                        Contact = contact,
                        ContactID = contact.Id
                    };

                    context.AddRange(contact, address, addressAndContact);

                }*/
                using (var context = Fixture.CreateContext(transaction))
                {
                    //IRepository<Contact> _contactRepository = new 





                    /*var contact = await context.Contacts.FirstOrDefaultAsync(e=>e.FirstName == "John");
                    contact.ShouldNotBeNull();
                    contact.FirstName.ShouldBe("John");
                    var contactDto = new ContactDto() { Id = contact.Id, FirstName = contact.FirstName, LastName = contact.LastName, DateOfBirth = contact.DateOfBirth };

                    var list = await _contactAppService.GetContacts();
                    list.Contains(contactDto).ShouldBeTrue();

                    await _contactAppService.DeleteContact(contact.Id);

                    var delContact = await context.Contacts.FirstOrDefaultAsync(e => e.FirstName == "John");
                    delContact.ShouldNotBe(contact);*/


                    /*var outp = context.Contacts.Select(e=>new ContactDto() {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        DateOfBirth = e.DateOfBirth,
                    });
                    output.ShouldNotBeEmpty();*/
                    //outp.FirstName.ShouldBe("John");
                    //Assert
                    //output.Count().ShouldBeGreaterThan(0);
                }
            }
        }

        [Fact]
        public async Task CreateContact_Test()
        {
            await _contactAppService.CreateContact(
                new CreateContactInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001-05-01)
                });

            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(f => f.FirstName == "John" && f.LastName == "Frill");
                sampleContact.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task CreateContactAndAddress_Test() //ask sofynae about using the context throughout the whole function
        {
            await _contactAppService.CreateContactAndAddress(
                new CreateContactAndAddressInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01),
                    AddressLine = "34 Fragile Lane"
                });

            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(f => f.FirstName == "John" && f.LastName == "Frill");
                var sampleAddress = await context.Addresses.FirstOrDefaultAsync(a => a.AddressString == "34 Fragile Lane");
                sampleContact.ShouldNotBeNull();
                sampleAddress.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task GetContacts_Test()
        {

            //Act
            await _contactAppService.CreateContact(
                new CreateContactInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01)
                });
            var output = await _contactAppService.GetContacts();

            //Assert
            output.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetContactAddresses_Test()
        {
            //Act
            await _contactAppService.CreateContactAndAddress(
                new CreateContactAndAddressInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01),
                    AddressLine = "34 Fragile Lane"
                });
            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(f => f.FirstName == "John" && f.LastName == "Frill");
                var output = await _contactAppService.GetContactAddresses(sampleContact.Id);
                output.ShouldNotBeNull();
            });
            
        }

        [Fact]
        public async Task GetAddressContacts_Test()
        {
            //Act
            await _contactAppService.CreateContactAndAddress(
                new CreateContactAndAddressInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01),
                    AddressLine = "34 Fragile Lane"
                });
            await UsingDbContextAsync(async context =>
            {
                var sampleAddress = await context.Addresses.FirstOrDefaultAsync(a => a.AddressString == "34 Fragile Lane");
                var output = await _contactAppService.GetAddressContacts(sampleAddress.Id);
                output.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task GetPhones_Test()
        {
            //Act
            await _contactAppService.CreateContactAndAddress(
                new CreateContactAndAddressInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01),
                    AddressLine = "34 Fragile Lane"
                });
            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(f => f.FirstName == "John" && f.LastName == "Frill");
                await _contactAppService.CreatePhone(
                new CreatePhoneInput
                {
                    PhoneNum = "08842995883",
                    Type = "Mobile",
                    ContactId = sampleContact.Id
                });
            });
            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(f => f.FirstName == "John" && f.LastName == "Frill");
                var output = await _contactAppService.GetPhones(new GetPhonesInput { FirstName = "John", LastName = "Frill", ContactId = sampleContact.Id });
                output.ShouldNotBeNull();
            });
            /*using (var context = new SharedDatabaseFixture())
            {

            }*/
        }

        [Fact]
        public async Task CreatePhone_Test()
        {
            //Act
            await _contactAppService.CreateContactAndAddress(
                new CreateContactAndAddressInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01),
                    AddressLine = "34 Fragile Lane"
                });
            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(f => f.FirstName == "John" && f.LastName == "Frill");
                await _contactAppService.CreatePhone(
                new CreatePhoneInput
                {
                    PhoneNum = "08842995883",
                    Type = "Mobile",
                    ContactId = sampleContact.Id
                });

                sampleContact.ShouldNotBeNull();
                var samplePhone = await context.Phones.FirstOrDefaultAsync(p => p.PhoneNum == "08842995883");
                samplePhone.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task EditContact_Test()
        {
            await _contactAppService.CreateContact(
                new CreateContactInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01)
                });
            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(c => c.FirstName == "John" && c.LastName == "Frill");

                await _contactAppService.EditContact(new EditContactInput { Id = sampleContact.Id, FirstName = "Johnny", LastName = "Frilt", DateOfBirth = new DateTime(2001-03-01) });

                context.SaveChanges();
                var editContact = await context.Contacts.FirstOrDefaultAsync(c => c.FirstName == "Johnny" && c.LastName == "Frilt");
                
                editContact.FirstName.ShouldBe("Johnny");
                editContact.LastName.ShouldBe("Frilt");
                editContact.DateOfBirth.ToString().ShouldBe(new DateTime(2001 - 03 - 01).ToString());
            });
        }

        [Fact]
        public async Task EditAddress_Test()
        {
            await _contactAppService.CreateContactAndAddress(
                new CreateContactAndAddressInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01),
                    AddressLine = "34 Fragile Lane"
                });
            await UsingDbContextAsync(async context =>
            {
                var sampleAddress = await context.Addresses.FirstOrDefaultAsync(a => a.AddressString == "34 Fragile Lane");
                await _contactAppService.EditAddress(new EditAddressInput { Id = sampleAddress.Id, AddressString = "35 Fragile Lane" });
                await context.SaveChangesAsync();

                var editAddress = await context.Addresses.FirstOrDefaultAsync(a => a.AddressString == "35 Fragile Lane");
                editAddress.AddressString.ShouldBe("35 Fragile Lane");
            });
        }

        [Fact]
        public async Task EditPhone_Test()
        {
            await UsingDbContextAsync(async context =>
            {
                var samplePhone = await context.Phones.FirstOrDefaultAsync(p => p.PhoneNum == "08842995883");
                await _contactAppService.EditPhone(new EditPhoneInput { Id = samplePhone.Id, PhoneNum = "00888999333", Type = "Mobile" });
                var editPhone = await context.Phones.FirstOrDefaultAsync(p => p.PhoneNum == "00888999333");
                editPhone.PhoneNum.ShouldBe("00888999333");
            });
        }

        [Fact]
        public async Task DeleteContact_Test()
        {
            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(c => c.FirstName == "John" && c.LastName == "Frilt");
                await _contactAppService.DeleteContact(sampleContact.Id);
                var deletedContact = await context.Contacts.FirstOrDefaultAsync(c => c.FirstName == "John" && c.LastName == "Frilt");
                deletedContact.ShouldBeNull();
            });
        }

        [Fact]
        public async Task DeleteAddress_Test()
        {
            await _contactAppService.CreateContactAndAddress(
                new CreateContactAndAddressInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01),
                    AddressLine = "34 Fragile Lane"
                });
            await UsingDbContextAsync(async context =>
            {
                var sampleAddress = await context.Addresses.FirstOrDefaultAsync(a => a.AddressString == "34 Fragile Lane");
                //await _contactAppService.DeleteContact(sampleAddress.Id);
                context.Addresses.Remove(sampleAddress);
                var deletedAddress = await context.Addresses.FirstOrDefaultAsync(a => a.AddressString == "34 Fragile Lane");
                deletedAddress.ShouldBeNull();
            });
            await UsingDbContextAsync(async context =>
            {
                var isDeleted = await context.Addresses.FirstOrDefaultAsync(a => a.AddressString == "34 Fragile Lane");
                isDeleted.ShouldBeNull();
            });
        }

        [Fact]
        public async Task DeletePhone_Test()
        {
            await UsingDbContextAsync(async context =>
            {
                var samplePhone = await context.Phones.FirstOrDefaultAsync(p => p.PhoneNum == "00888999333");
                await _contactAppService.DeletePhone(samplePhone.Id);
                var deletedPhone = await context.Phones.FirstOrDefaultAsync(p => p.PhoneNum == "00888999333");
                deletedPhone.ShouldBeNull();
            });
        }

        [Fact]
        public async Task DeletePhones_Test()
        {
            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(c => c.FirstName == "John" && c.LastName == "Frill");
                await _contactAppService.DeletePhones(sampleContact.Id);
                var phonesContact = await context.Contacts.FirstOrDefaultAsync(c => c.FirstName == "John" && c.LastName == "Frill");
                var deletedPhones = phonesContact.Phones;
                deletedPhones.Count().ShouldBe(0);
            });
        }

        [Fact]
        public async Task AddAddressToContact_Test()
        {
            await _contactAppService.CreateContactAndAddress(
                new CreateContactAndAddressInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01),
                    AddressLine = "34 Fragile Lane"
                });
            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(c => c.FirstName == "John" && c.LastName == "Frill");
                await _contactAppService.AddAddressToContact(new AddAddressToContactInput() { ContactId = sampleContact.Id, AddressLine = "13 Hallow End" });
                var contactAddress = await _contactAppService.GetContactAddresses(sampleContact.Id);
                var sampleAddress = await context.Addresses.FirstOrDefaultAsync(a => a.AddressString == "13 Hallow End");
                sampleAddress.AddressString.ShouldBe("13 Hallow End");
                contactAddress.FindAll(a => a.AddressString == "13 Hallow End").Count().ShouldBe(1);
            });
        }

        [Fact]
        public async Task RemoveAddressFromContact_Test()
        {
            await _contactAppService.CreateContactAndAddress(
                new CreateContactAndAddressInput
                {
                    FirstName = "John",
                    LastName = "Frill",
                    DateOfBirth = new DateTime(2001 - 05 - 01),
                    AddressLine = "13 Hallow End"
                });
            await UsingDbContextAsync(async context =>
            {
                var sampleContact = await context.Contacts.FirstOrDefaultAsync(c => c.FirstName == "John" && c.LastName == "Frill");
                var sampleAddress = await context.Addresses.FirstOrDefaultAsync(a => a.AddressString == "13 Hallow End");
                await _contactAppService.RemoveAddressFromContact(new RemoveAddressFromContactInput() { ContactId = sampleContact.Id, AddressId = sampleAddress.Id });
                var contactAddress = await _contactAppService.GetContactAddresses(sampleContact.Id);
                contactAddress.Contains(new AddressDto() { Id = sampleAddress.Id, AddressString = sampleAddress.AddressString }).ShouldBeFalse();
            });
        }
        /*
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
        */
    }
}
