namespace Domain.Entities.Vehicles
{
    public class Hatchback : Vehicle
    {
        public Hatchback(string manufacturer, string model, float startingBid, int year, int numberOfDoors) : base(manufacturer, model, startingBid, year, VehicleType.HATCHBACK)
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
