using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.ContactManagement.DTOs
{
    public class EditAddressInput : EntityDto
    {
        public string AddressString { get; set; }
    }
}
