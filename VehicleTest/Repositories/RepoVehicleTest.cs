using Domain.Entities.Vehicles;
using Persistence.Repositories;

namespace VehicleTest.Repositories
{
    [TestClass]
    public class RepoVehicleTest
    {
        private VehicleRepository _repo = new VehicleRepository();

        [TestInitialize]
        public void Startup()
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
            var result = _repo.QueryVehicles();
            Assert.AreEqual(8, result.ToList().Count());
        }

        [TestMethod]
        public void CreateVehicle()
        {
            Vehicle vehicle = new SUV("BMW", "222", 0, 10, 4);
            var query = _repo.QueryVehicles();
            Assert.AreEqual(8, query.ToList().Count()); //  start with 8

            var result = _repo.AddVehicle(vehicle).Result; 
            Assert.IsTrue(result);
            query = _repo.QueryVehicles();
            Assert.AreEqual(9, query.ToList().Count()); //  add 1 and recieve 9
        }
    }
}