using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Vehicles
{
    public abstract class Vehicle
    {
        public Vehicle(string manufacturer, string model, float startingBid, int year, VehicleType type)
        {
            Id = Guid.NewGuid();
            Year = year;
            Manufacturer = manufacturer;
            Model = model;
            StartingBid = startingBid;
            Type = type;
        }
        public readonly Guid Id;
        public readonly string? Manufacturer;
        public readonly string? Model;
        public readonly float StartingBid;
        [Range(float.MinValue, 0, ErrorMessage = "Year must be higher than zero.")]
        public readonly int Year;
        public readonly VehicleType Type;

        public override bool Equals(object? obj)
        {
            return (obj is Vehicle vehicle && 
                this.Id == vehicle.Id && 
                this.Manufacturer == vehicle.Manufacturer && 
                this.Model == vehicle.Model && 
                this.StartingBid == vehicle.StartingBid && 
                this.Type == vehicle.Type && 
                this.Year == vehicle.Year) ;
        }
    }
    public enum VehicleType : byte
    {
        SUV,
        SEDAN,
        HATCHBACK,
        TRUCK
    }
}
