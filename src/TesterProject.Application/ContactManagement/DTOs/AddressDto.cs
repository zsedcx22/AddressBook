using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement.DTOs
{
    public class AddressDto : EntityDto  //maybe set addresses in contact to the addresses linked with the selected contact
    {
        public string AddressString { get; set; }
        //public ICollection<AddressAndContact> AddressAndContacts { get; set; } //not sure if I need the linker
    }
}
