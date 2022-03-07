using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.CarManagement.DTOs
{
    public class CarDto : EntityDto
    {
        public string Make { get; set; }
        public string Colour { get; set; }
    }
}
