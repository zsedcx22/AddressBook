﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.ContactManagement.DTOs
{
    public class AddAddressToContactInput
    {
        public int ContactId { get; set; }
        public string AddressLine { get; set; }
    }
}
