namespace Domain.Entities.Vehicles
{
    public class Sedan : Vehicle
    {
        public Sedan(string manufacturer, string model, float startingBid, int year, int numberOfDoors) : base(manufacturer, model, startingBid, year, VehicleType.SEDAN)
        {
            if (numberOfDoors < 0 || numberOfDoors % 2 != 0)
            {
                throw new Exception("The number of doors must be higher than zero and an even number.");
            }
            NumberOfDoors = numberOfDoors;
        }

        public int NumberOfDoors { get; set; }
    }
}
