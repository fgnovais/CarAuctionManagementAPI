using Domain.Entities.Auctions;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly List<Auction> _auctions = new List<Auction>();
        private readonly object _auctionsLock = new();
        public Task<bool> StartAuction(Auction auction)
        {
            _auctions.Add(auction);
            return Task.FromResult(true);
        }

        public Task<bool> UpdateAuction(Auction auction)
        {
            var auctionId = _auctions.FindIndex(a => a.VehicleId == auction.VehicleId);
            if (auctionId == -1) // auction with the given vehicleId wasn't found
            {
                return Task.FromResult(false);
            }

            lock (_auctionsLock) // avoid racing condition
            {
                _auctions[auctionId] = auction;
            }
            return Task.FromResult(true);
        }

        public Task<Auction>? GetAuctionByVehicleId(Guid vehicleId)
        {
            var auctionId = _auctions.FindIndex(a => a.VehicleId == vehicleId);
            if (auctionId == -1) // this auction doesn't exist
            {
                return null;
            }

            return Task.FromResult(_auctions[auctionId]);
        }
    }
}
