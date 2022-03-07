using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.ContactManagement.Entities;
using TesterProject.EntityFrameworkCore;

namespace TesterProject.Tests.AddressBook
{
    public class SharedDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        public SharedDatabaseFixture()
        {
            Connection = new SqlConnection(@"Server=.;Database=ContactDBTest;Integrated Security=true;MultipleActiveResultSets=true;");

            Seed();

            Connection.Open();
        }

        public DbConnection Connection { get; }

        public TesterProjectDbContext CreateContext(DbTransaction transaction = null)
        {
            var context = new TesterProjectDbContext(new DbContextOptionsBuilder<TesterProjectDbContext>().UseSqlServer(Connection).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        
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
                        /*
                        var contactId = CreateContact(new CreateContactInput()
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
                            addressId = _addressRepository.InsertAndGetIdAsync(new Address()
                            {
                                AddressString = input.AddressLine
                            });
                        }
                        _contactManager.AssignContactAddressAsync(addressId, contactId);*/
                        /*var one = new Item("ItemOne");
                        one.AddTag("Tag11");
                        one.AddTag("Tag12");
                        one.AddTag("Tag13");

                        var two = new Item("ItemTwo");

                        var three = new Item("ItemThree");
                        three.AddTag("Tag31");
                        three.AddTag("Tag31");
                        three.AddTag("Tag31");
                        three.AddTag("Tag32");
                        three.AddTag("Tag32");

                        context.AddRange(one, two, three);*/

                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public void Dispose() => Connection.Dispose();
    }
}
