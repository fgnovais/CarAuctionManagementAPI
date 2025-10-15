using Application.DTO;
using Application.Errors;
using Domain.Entities.Auctions;
using Domain.Repositories;

namespace Application.UseCases.Auctions
{
    public class CloseAuction : ICloseAuction
    {
        private readonly IAuctionRepository _repo;
        public CloseAuction(IAuctionRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<AuctionDTO, Error>> Handle(Guid vehicleId)
        {
            var auction = _repo.GetAuctionByVehicleId(vehicleId)?.Result;

            if (auction == null) // auction with the given vehicleId wasn't found
            {
                return Result<AuctionDTO, Error>.Failure(AuctionErrors.NotFound);
            }

            if (auction.Status == AuctionStatus.CLOSED)
            {
                return Result<AuctionDTO, Error>.Failure(AuctionErrors.AlreadyClosed);
            }

            auction.Status = AuctionStatus.CLOSED;
            var updateResult = await _repo.UpdateAuction(auction);

            if (!updateResult)
            {
                return Result<AuctionDTO, Error>.Failure(AuctionErrors.FailedToUpdate);
            }
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
