using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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
    public class CarAppService : TesterProjectAppServiceBase, ICarAppService
    {
        private readonly IRepository<Car> _carRepository;
        public CarAppService(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task CreateCar(CreateCarInput input) //Create a new car entity
        {
            var tempCar = new Car { Colour = input.Colour, Make = input.Make, ContactId = input.ContactId };
            await _carRepository.InsertAsync(tempCar);
        }

        public async Task EditCar(EditCarInput input) //Edit a car based on its ID
        {
            var edit = await _carRepository.GetAsync(input.Id);
            edit.Colour = input.Colour;
            edit.Make = input.Make;
        }

        public async Task<List<CarDto>> GetCars(int contactId) //return all cars associated with a contact using its ID
        {
            var output = await _carRepository.GetAll().Where(e => e.ContactId == contactId).ToListAsync();
            return output.Select(e => new CarDto() { Id = e.Id, Colour = e.Colour, Make = e.Make }).ToList();
        }

        public async Task DeleteCar(int carId) //delete a car based on its ID
        {
            await _carRepository.DeleteAsync(carId);
        }

        public async Task DeleteCars(int contactId) //delete all cars associated with a contact - to be used after a contact is deleted
        {
            var delList = await _carRepository.GetAllListAsync(e => e.ContactId == contactId);
            foreach (Car car in delList)
            {
                await _carRepository.DeleteAsync(car);
            }
        }
    }
}
