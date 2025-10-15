namespace Domain.Entities.Vehicles
{
    public class Truck : Vehicle
    {
        public Truck(string manufacturer, string model, float startingBid, int year, float loadCapacity) : base(manufacturer, model, startingBid, year, VehicleType.TRUCK)
        {
            if (loadCapacity < 0)
            {
                throw new Exception("Load capacity can't be smaller than zero.");
            }
            LoadCapacity = loadCapacity;
        }

        public float LoadCapacity { get; set; }
    }
}
