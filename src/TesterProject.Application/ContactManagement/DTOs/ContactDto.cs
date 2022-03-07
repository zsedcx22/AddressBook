using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.CarManagement.DTOs;
using TesterProject.CarManagement.Entities;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement.DTOs
{
    public class ContactDto : EntityDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        /*public List<Address> Addresses { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Car> Cars { get; set; }*/

    }
}
