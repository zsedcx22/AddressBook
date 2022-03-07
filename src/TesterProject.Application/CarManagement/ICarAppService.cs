using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.CarManagement.DTOs;
using TesterProject.CarManagement.Entities;
using TesterProject.ContactManagement.DTOs;

namespace TesterProject.CarManagement
{
    public interface ICarAppService:IApplicationService
    {
        Task<List<CarDto>> GetCars(int contactId);
        Task CreateCar(CreateCarInput input);
        Task EditCar(EditCarInput input);
        Task DeleteCar(int carId);
        Task DeleteCars(int contactId);
    }
}
