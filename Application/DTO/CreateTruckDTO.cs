using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class CreateTruckDTO : CreateVehicleRequestDTO
    {
        [Range(0, float.MaxValue, ErrorMessage = "LoadCapacity must be a positive number.")]
        public required float LoadCapacity { get; set; }
    }
}
