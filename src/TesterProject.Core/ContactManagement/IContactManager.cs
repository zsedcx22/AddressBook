using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement
{
    public interface IContactManager
    {
        Task AssignContactAddressAsync(int addressId, int contactId);
        Task DeleteAddressAndContacts(int contactId);
        Task RemoveContactAddressAsync(int addressId, int contactId);
        Task<int> CountAddressAndContact(int addressId);
    }
}
