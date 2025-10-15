using Domain.Entities.Vehicles;

namespace VehicleTest.Domain
{
    [TestClass]

    public class DomainVehicleTest
    {
        #region Vehicle Creation
        [TestMethod]
        public void VehicleCreationSedan()
        {
            var vehicle = new Sedan("Fiat", "500", 0f, 2025, 4);
            Assert.AreEqual("Fiat", vehicle.Manufacturer);
            Assert.AreEqual("500", vehicle.Model);
            Assert.AreEqual(0.00f, vehicle.StartingBid);
            Assert.AreEqual(2025, vehicle.Year);
            Assert.AreEqual(4, vehicle.NumberOfDoors);
            Assert.IsTrue(vehicle is Sedan);

            vehicle = new Sedan("BMW", "100", 1000f, 199, 2);
            Assert.AreNotEqual("Fiat", vehicle.Manufacturer);
            Assert.AreNotEqual("500", vehicle.Model);
            Assert.AreNotEqual(0.00f, vehicle.StartingBid);
            Assert.AreNotEqual(2025, vehicle.Year);
            Assert.AreNotEqual(4, vehicle.NumberOfDoors);
            Assert.IsTrue(vehicle is Sedan);

            Assert.ThrowsException<Exception>(() => new Sedan("Fiat", "500", 0f, 2025, -3));
        }
        [TestMethod]
        public void VehicleCreationSUV()
        {
            var vehicle = new SUV("Fiat", "500", 200f, 2025, 3);

            Assert.AreEqual("Fiat", vehicle.Manufacturer);
            Assert.AreEqual("500", vehicle.Model);
            Assert.AreEqual(200.00f, vehicle.StartingBid);
            Assert.AreEqual(2025, vehicle.Year);
            Assert.AreEqual(3, vehicle.NumberOfSeats);
            Assert.IsTrue(vehicle is SUV);

            vehicle = new SUV("BMW", "100", 1000f, 199, 2);
            Assert.AreNotEqual("Fiat", vehicle.Manufacturer);
            Assert.AreNotEqual("500", vehicle.Model);
            Assert.AreNotEqual(200.00f, vehicle.StartingBid);
            Assert.AreNotEqual(2025, vehicle.Year);
            Assert.AreNotEqual(3, vehicle.NumberOfSeats);
            Assert.IsTrue(vehicle is SUV);

            Assert.ThrowsException<Exception>(() => new SUV("Fiat", "500", 0f, 2025, -3));
        }
        [TestMethod]
        public void VehicleCreationTruck()
        {
            var vehicle = new Truck("Fiat", "500", 50f, 2020, 4);

            Assert.AreEqual("Fiat", vehicle.Manufacturer);
            Assert.AreEqual("500", vehicle.Model);
            Assert.AreEqual(50.00f, vehicle.StartingBid);
            Assert.AreEqual(2020, vehicle.Year);
            Assert.AreEqual(4, vehicle.LoadCapacity);
            Assert.IsTrue(vehicle is Truck);

            vehicle = new Truck("BMW", "100", 1000f, 199, 2);
            Assert.AreNotEqual("Fiat", vehicle.Manufacturer);
            Assert.AreNotEqual("500", vehicle.Model);
            Assert.AreNotEqual(50.00f, vehicle.StartingBid);
            Assert.AreNotEqual(2020, vehicle.Year);
            Assert.AreNotEqual(4, vehicle.LoadCapacity);
            Assert.IsTrue(vehicle is Truck);

            Assert.ThrowsException<Exception>(() => new Truck("Fiat", "500", 0f, 2025, -3));
        }

        [TestMethod]
        public void VehicleCreationHatchback()
        {
            var vehicle = new Hatchback("Fiat", "500", 0f, 2025, 4);

            Assert.AreEqual("Fiat", vehicle.Manufacturer);
            Assert.AreEqual("500", vehicle.Model);
            Assert.AreEqual(0.00f, vehicle.StartingBid);
            Assert.AreEqual(2025, vehicle.Year);
            Assert.AreEqual(4, vehicle.NumberOfDoors);
            Assert.IsTrue(vehicle is Hatchback);

            vehicle = new Hatchback("BMW", "100", 1000f, 199, 2);
            Assert.AreNotEqual("Fiat", vehicle.Manufacturer);
            Assert.AreNotEqual("500", vehicle.Model);
            Assert.AreNotEqual(0.00f, vehicle.StartingBid);
            Assert.AreNotEqual(2025, vehicle.Year);
            Assert.AreNotEqual(4, vehicle.NumberOfDoors);
            Assert.IsTrue(vehicle is Hatchback);
            
            Assert.ThrowsException<Exception>(() => new Hatchback("Fiat", "500", 0f, 2025, 3));
        }

        #endregion
    }
}
