using Application.DTO;
using Application.Errors;
using Application.UseCases.Vehicles;
using Domain.Entities.Vehicles;

namespace Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IGetFilteredVehicles getFilteredVehiclesUseCase;
        private readonly IAddVehicle createVehicleUseCase;

        public VehicleService(IGetFilteredVehicles filter, IAddVehicle create)
        {
            getFilteredVehiclesUseCase = filter;
            createVehicleUseCase = create;
        }
        public Task<Result<List<VehicleDTO>, Error>> GetFilteredVehicles(VehicleFilter filter)
        {
            return getFilteredVehiclesUseCase.Handle(filter);
        }

        public Task<Result<VehicleDTO, Error>> AddVehicle(Vehicle vehicle)
        {
            return createVehicleUseCase.Handle(vehicle);
        }
    }
}
