using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.CarManagement.Entities;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement.DTOs
{
    public class CreateContactInput
    { 
        [Required, MaxLength(15)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
