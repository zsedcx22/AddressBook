using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.CarManagement.Entities
{
    public class Car : Entity
    {
        public virtual string Make { get; set; }
        public virtual string Colour { get; set; }

        public virtual int ContactId { get; set; }
        [ForeignKey(nameof(ContactId))]
        public virtual Contact Contact { get; set; }
    }
}
