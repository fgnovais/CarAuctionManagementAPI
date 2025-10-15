using Domain.Repositories;
using Application.DTO;
using Application.Errors;
using Domain.Entities.Vehicles;

namespace Application.UseCases.Vehicles
{
    public class FilterVehicle : IGetFilteredVehicles
    {
        private readonly IVehicleRepository _repo;
        public FilterVehicle(IVehicleRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<VehicleDTO>, Error>> Handle(VehicleFilter filter)
        {
            var query = _repo.QueryVehicles();
            if (filter.ByYear.HasValue)
            {
                query = query.Where(v => v.Year == filter.ByYear);
            }

            if (filter.ByType.HasValue)
            {
                query = query.Where(v => v.Type == filter.ByType);
            }

            if (!string.IsNullOrWhiteSpace(filter.ByManufacturer))
            {
                query = query.Where(v => filter.ByManufacturer.Equals(v.Manufacturer, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter.ByModel))
            {
                query = query.Where(v => filter.ByModel.Equals(v.Model, StringComparison.OrdinalIgnoreCase));
            }

            var filteredVehicles = query.ToList();
            List<VehicleDTO> mappedVehicles = new();

            foreach (Vehicle filteredVehicle in filteredVehicles)
            {
                mappedVehicles.Add(new VehicleDTO
                {
                    Id = filteredVehicle.Id,
                    Manufacturer = filteredVehicle.Manufacturer,
                    Model = filteredVehicle.Model,
                    Year = filteredVehicle.Year,
                    StartingBid = filteredVehicle.StartingBid,
                    Type = filteredVehicle.Type,
                });
            }
            
            return Result<List<VehicleDTO>, Error>.Success(mappedVehicles);
        }
    }
}
