using Application.DTO;
using Application.Errors;
using Domain.Entities.Vehicles;

namespace Application.UseCases.Vehicles
{
    public interface IAddVehicle
    {
        Task<Result<VehicleDTO, Error>> Handle(Vehicle vehicle);
    }

    public interface IGetFilteredVehicles
    {
        Task<Result<List<VehicleDTO>, Error>> Handle(VehicleFilter filter);
    }
}
