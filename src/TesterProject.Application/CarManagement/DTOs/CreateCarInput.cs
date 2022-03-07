using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.CarManagement.DTOs
{
    public class CreateCarInput
    {
        public string Make { get; set; }
        public string Colour { get; set; }
        public int ContactId { get; set; }
    }
}
