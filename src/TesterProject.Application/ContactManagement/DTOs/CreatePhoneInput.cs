using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.ContactManagement.DTOs
{
    public class CreatePhoneInput
    {
        public string Type { get; set; }
        public string PhoneNum { get; set; }
        public int ContactId { get; set; }

    }
}
