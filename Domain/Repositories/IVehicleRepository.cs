using Domain.Entities.Vehicles;

namespace Domain.Repositories
{
    public interface IVehicleRepository
    {
        public IQueryable<Vehicle> QueryVehicles();
        public Task<bool> AddVehicle(Vehicle vehicle);
    }
}
