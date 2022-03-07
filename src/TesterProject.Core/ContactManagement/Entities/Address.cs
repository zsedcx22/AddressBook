using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.ContactManagement.Entities
{
    public class Address : Entity
    {
        public virtual string AddressString { get; set; }

        public virtual ICollection<AddressAndContact> AddressAndContacts { get; set; }
    }
}
