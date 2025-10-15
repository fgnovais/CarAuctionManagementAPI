using Application.DTO;
using Application.Errors;
using Domain.Entities.Vehicles;

namespace Application.Services
{
    public interface IVehicleService
    {
        Task<Result<List<VehicleDTO>, Error>> GetFilteredVehicles(VehicleFilter filter);
        Task<Result<VehicleDTO, Error>> AddVehicle(Vehicle vehicle);
    }
}
