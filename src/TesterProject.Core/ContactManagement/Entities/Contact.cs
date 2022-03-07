using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.CarManagement.Entities;

namespace TesterProject.ContactManagement.Entities
{
    [Index(nameof(FirstName), nameof(LastName))]
    public class Contact : FullAuditedEntity, IPassivable
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual bool IsActive { get; set; } = true;

        public virtual ICollection<AddressAndContact> AddressAndContacts { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
