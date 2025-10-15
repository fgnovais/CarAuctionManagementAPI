using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class CreateHatchbackDTO : CreateVehicleRequestDTO
    {
        [Range(0, int.MaxValue, ErrorMessage = "NumberOfDoors must be a positive number.")]
        public required int NumberOfDoors { get; set; }
    }
}
