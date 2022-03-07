using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.ContactManagement.DTOs
{
    public class EditPhoneInput : EntityDto
    {
        public string Type { get; set; }
        public string PhoneNum { get; set; }
    }
}
