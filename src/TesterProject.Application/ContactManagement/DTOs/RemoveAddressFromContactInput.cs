using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.ContactManagement.DTOs
{
    public class RemoveAddressFromContactInput
    {
        public int ContactId { get; set; }
        public int AddressId { get; set; }
    }
}
