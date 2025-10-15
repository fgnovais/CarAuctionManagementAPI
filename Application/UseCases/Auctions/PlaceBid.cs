using Application.DTO;
using Application.Errors;
using Domain.Entities.Auctions;
using Domain.Repositories;

namespace Application.UseCases.Auctions
{
    public class PlaceBid : IPlaceBidOnAuction
    {
        private readonly IAuctionRepository _repo;

        public PlaceBid(IAuctionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<AuctionDTO, Error>> Handle(Guid vehicleId, float bid)
        {
            var auction = _repo.GetAuctionByVehicleId(vehicleId)?.Result;

            if (auction == null) // auction with the given vehicleId wasn't found
            {
                return Result<AuctionDTO, Error>.Failure(AuctionErrors.NotFound);
            }

            if (auction.Status == AuctionStatus.CLOSED)
            {
                return Result<AuctionDTO, Error>.Failure(AuctionErrors.AuctionClosed);
            }

            var result = auction.BidOnAuction(bid);

            if (!result)
            {
                return Result<AuctionDTO, Error>.Failure(AuctionErrors.SmallerBid);
            }

            await _repo.UpdateAuction(auction);

            var mappedAuction = new AuctionDTO
            {
                VehicleId = auction.VehicleId,
                CurrentBid = auction.CurrentBid,
                Status = auction.Status
            };

            return Result<AuctionDTO, Error>.Success(mappedAuction);
        }
    }
}
