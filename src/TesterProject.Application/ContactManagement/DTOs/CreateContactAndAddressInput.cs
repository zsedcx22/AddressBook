using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.ContactManagement.DTOs
{
    public class CreateContactAndAddressInput
    {
        [Required, MaxLength(15)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressLine { get; set; }
    }
}
