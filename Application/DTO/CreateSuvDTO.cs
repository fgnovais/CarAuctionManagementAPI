using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class CreateSuvDTO : CreateVehicleRequestDTO
    {
        [Range(0, float.MaxValue, ErrorMessage = "NumberOfSeats must be a positive number.")]
        public required int NumberOfSeats { get; set; }
    }
}
