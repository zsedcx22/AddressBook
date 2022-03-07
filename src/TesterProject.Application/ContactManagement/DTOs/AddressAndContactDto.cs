using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement.DTOs
{
    public class AddressAndContactDto : EntityDto
    {
        public Address Address { get; set; }
        public Contact Contact { get; set; }
    }
}
