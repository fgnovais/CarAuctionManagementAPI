using Domain.Entities.Vehicles;

namespace Application.DTO
{
    public class VehicleDTO
    {
        public Guid Id { get; set; }
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public float StartingBid { get; set; }
        public int Year { get; set; }
        public VehicleType Type { get; set; }

        public override bool Equals(object? obj)
        {
            return (obj is VehicleDTO vehicle &&
                this.Id == vehicle.Id &&
                this.Manufacturer == vehicle.Manufacturer &&
                this.Model == vehicle.Model &&
                this.StartingBid == vehicle.StartingBid &&
                this.Type == vehicle.Type &&
                this.Year == vehicle.Year);
        }
    }
}
