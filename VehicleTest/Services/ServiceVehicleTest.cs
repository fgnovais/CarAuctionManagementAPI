using Application.DTO;
using Application.UseCases.Vehicles;
using Domain.Entities.Vehicles;
using Persistence.Repositories;

namespace VehicleTest.Services
{
    [TestClass]
    public class ServiceVehicleTest
    {
        private static VehicleRepository _repo = new VehicleRepository();
        private FilterVehicle filterUseCase = new FilterVehicle(_repo);
        private AddVehicle addVehicleUseCase = new AddVehicle(_repo);

        [TestInitialize]
        public void TestInit()
        {
            _repo.AddVehicle(new Hatchback("Fiat", "500", 5000, 2012, 2));
            _repo.AddVehicle(new Truck("Fiat", "500", 1000, 2022, 100));
            _repo.AddVehicle(new Sedan("Fiat", "500", 400, 2015, 8));
            _repo.AddVehicle(new SUV("Fiat", "500", 0, 1999, 4));
            _repo.AddVehicle(new Hatchback("Peogeout", "308", 100, 2011, 2));
            _repo.AddVehicle(new Truck("Peogeout", "308", 2, 2022, 100));
            _repo.AddVehicle(new Sedan("BMW", "222", 5, 2015, 8));
            _repo.AddVehicle(new SUV("BMW", "222", 0, 10, 4));
        }

        [TestMethod]
        public void QueryVehicles()
        {
            VehicleFilter filter = new VehicleFilter();
            var result = filterUseCase.Handle(filter).Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(17, result._value.Count());

            filter = new VehicleFilter(byType: VehicleType.SUV);
            result = filterUseCase.Handle(filter).Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(4, result._value.Count());

            filter = new VehicleFilter(byManufacturer: "Peogeout");
            result = filterUseCase.Handle(filter).Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(4, result._value.Count());

            filter = new VehicleFilter(byModel: "308");
            result = filterUseCase.Handle(filter).Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(4, result._value.Count());

            filter = new VehicleFilter(byYear: 2012);
            result = filterUseCase.Handle(filter).Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result._value.Count());
        }

        [TestMethod]
        public void AddVehicles()
        {
            Vehicle vehicle = new Sedan("Fiat", "500", 400, 2015, 8);
            var result = addVehicleUseCase.Handle(vehicle).Result;
            Assert.IsTrue(result.IsSuccess);

            VehicleDTO vehicleDTO = new VehicleDTO
            {
                Id = vehicle.Id,
                Manufacturer = vehicle.Manufacturer,
                Model = vehicle.Model,
                Year = vehicle.Year,
                StartingBid = vehicle.StartingBid,
                Type = vehicle.Type,
            };

            Assert.AreEqual(vehicleDTO, result._value);
        }
    }
}
