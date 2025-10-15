namespace Domain.Entities.Vehicles
{
    public struct VehicleFilter
    {
        public VehicleFilter(VehicleType? byType = null, string? byManufacturer = "", string? byModel = "", int? byYear = null)
        {
            ByType = byType;
            ByManufacturer = byManufacturer;
            ByModel = byModel;
            ByYear = byYear;
        }
        public VehicleType? ByType;
        public string? ByManufacturer;
        public string? ByModel;
        public int? ByYear;
    }
}
