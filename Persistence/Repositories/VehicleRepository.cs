using Domain.Entities.Vehicles;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly List<Vehicle> _vehicles = new List<Vehicle>();
        private readonly object vehicleLock = new();

        public IQueryable<Vehicle> QueryVehicles()
        {
            var query = _vehicles.AsQueryable();
            return query;
        }

        public Task<bool> AddVehicle(Vehicle vehicle)
        {
            lock (vehicleLock)
            {
                // because a Guid was generated, the chance of a vehicle having the same id is zero.
                if (_vehicles.Any(v => v.Id == vehicle.Id))
                {
                    return Task.FromResult(false);
                }

                _vehicles.Add(vehicle);
            }
            return Task.FromResult(true);
        }
    }
}
