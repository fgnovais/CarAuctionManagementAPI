using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTO
{
    [JsonPolymorphic]
    [JsonDerivedType(typeof(CreateSuvDTO), typeDiscriminator: "SUV")] // if the client sends type = SUV, the type created will be CreateSuvDTO
    [JsonDerivedType(typeof(CreateTruckDTO), typeDiscriminator: "TRUCK")]
    [JsonDerivedType(typeof(CreateSedanDTO), typeDiscriminator: "SEDAN")]
    [JsonDerivedType(typeof(CreateHatchbackDTO), typeDiscriminator: "HATCHBACK")]
    public class CreateVehicleRequestDTO
    {
       // public VehicleType Type { get; set; }
        // not needed because the client already sends the type (for polymorphic binding)
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public required int Year { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "StartingBid must be a positive number.")]
        public required float StartingBid { get; set; }
    }
}
