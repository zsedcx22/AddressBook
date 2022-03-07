using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesterProject.ContactManagement.Entities
{
    public class Phone : Entity
    {
        public virtual string Type { get; set; }
        public virtual string PhoneNum { get; set; }

        public virtual int ContactId { get; set; }
        [ForeignKey(nameof(ContactId))]
        public virtual Contact Contact { get; set; }

    }
}
