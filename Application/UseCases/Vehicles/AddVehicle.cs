using Application.DTO;
using Application.Errors;
using Domain.Entities.Vehicles;
using Domain.Repositories;

namespace Application.UseCases.Vehicles
{
    public class AddVehicle : IAddVehicle
    {
        private readonly IVehicleRepository _repo;

        public AddVehicle(IVehicleRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<VehicleDTO, Error>> Handle(Vehicle vehicle)
        {
            var result = await _repo.AddVehicle(vehicle);

            if (result)
            {
                var mappedVehicle = new VehicleDTO
                {
                    Id = vehicle.Id,
                    Manufacturer = vehicle.Manufacturer,
                    Model = vehicle.Model,
                    Year = vehicle.Year,
                    StartingBid = vehicle.StartingBid,
                    Type = vehicle.Type,
                };
                return Result<VehicleDTO, Error>.Success(mappedVehicle);
            }
            else
            {
                return Result<VehicleDTO, Error>.Failure(VehicleErrors.FailedToCreate);
            }
        }
    }
}
