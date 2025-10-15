using Domain.Entities.Vehicles;

namespace Testing.Test.Vehicle
{
    [TestClass]

    public class VehicleTest
    {
        #region Vehicle Creation
        [TestMethod]
        public void VehicleCreationSedan()
        {
            var result = new Sedan("Fiat", "500", 0f, 2025, 4);

            Assert.AreEqual("Fiat", result.Manufacturer);
            Assert.AreEqual("500", result.Model);
            Assert.AreEqual(10.00f, result.StartingBid);
            Assert.AreEqual(2025, result.Year);
            Assert.AreEqual(2, result.NumberOfDoors);
            Assert.IsFalse(result is Sedan);
        }
        [TestMethod]
        public void VehicleCreationSUV()
        {
            var result = new SUV("Fiat", "500", 0f, 2025, 4);

            Assert.AreEqual("Fiat", result.Manufacturer);
            Assert.AreEqual("500", result.Model);
            Assert.AreEqual(10.00f, result.StartingBid);
            Assert.AreEqual(2025, result.Year);
            Assert.AreEqual(2, result.NumberOfSeats);
            Assert.IsTrue(result is SUV);
        }
        [TestMethod]
        public void VehicleCreationTruck()
        {
            var result = new Truck("Fiat", "500", 0f, 2025, 4);

            Assert.AreEqual("Fiat", result.Manufacturer);
            Assert.AreEqual("500", result.Model);
            Assert.AreEqual(10.00f, result.StartingBid);
            Assert.AreEqual(2025, result.Year);
            Assert.AreEqual(2, result.LoadCapacity);
            Assert.IsTrue(result is Truck);
        }

        [TestMethod]
        public void VehicleCreationHatchback()
        {
            var result = new Hatchback("Fiat", "500", 0f, 2025, 4);

            Assert.AreEqual("Fiat", result.Manufacturer);
            Assert.AreEqual("500", result.Model);
            Assert.AreEqual(10.00f, result.StartingBid);
            Assert.AreEqual(2025, result.Year);
            Assert.AreEqual(2, result.NumberOfDoors);
            Assert.IsTrue(result is Hatchback);
        }
        #endregion

        #region GetVehicleFromFilter
        #endregion
    }
}
