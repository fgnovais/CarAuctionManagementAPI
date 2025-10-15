using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class PlaceBidDTO
    {
        public required Guid VehicleId { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Bid must be higher than zero.")]
        public required float Bid{ get; set; }
    }
}
