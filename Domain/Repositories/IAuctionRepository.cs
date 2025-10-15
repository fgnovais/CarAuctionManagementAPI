using Domain.Entities.Auctions;

namespace Domain.Repositories
{
    public interface IAuctionRepository
    {
        public Task<bool> StartAuction(Auction auction);
        public Task<bool> UpdateAuction(Auction auction);
        public Task<Auction>? GetAuctionByVehicleId(Guid vehicleId);
    }
}
