using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement.DTOs
{
    public class CreateAddressAndContactInput
    {
        public int AddressId { get; set; }
        public int ContactId { get; set; }
    }
}
