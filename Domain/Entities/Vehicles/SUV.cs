namespace Domain.Entities.Vehicles
{
    public class SUV : Vehicle
    {
        public SUV(string manufacturer, string model, float startingBid, int year, int numberOfSeats) : base(manufacturer, model, startingBid, year, VehicleType.SUV)
        {
            if (numberOfSeats < 1)
            {
                throw new Exception("The number of seats must be higher or equal to 1.");
            }
            NumberOfSeats = numberOfSeats;
        }

        public int NumberOfSeats { get; set; }
    }
}
