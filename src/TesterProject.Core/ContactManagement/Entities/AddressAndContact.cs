using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.ContactManagement.Entities
{
    public class AddressAndContact : Entity
    {
        [ForeignKey(nameof(AddressID))]
        public virtual Address Address { get; set; }
        public virtual int AddressID { get; set; }


        [ForeignKey(nameof(ContactID))]
        public virtual Contact Contact { get; set; }
        public virtual int ContactID { get; set; }

        public AddressAndContact()
        {

        }
        public AddressAndContact(int addressId, int contactId)
        {
            AddressID = addressId;
            ContactID = contactId;
        }
    }
}
