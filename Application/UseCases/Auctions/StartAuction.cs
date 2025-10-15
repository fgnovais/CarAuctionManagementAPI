using Application.DTO;
using Application.Errors;
using Domain.Entities.Auctions;
using Domain.Repositories;

namespace Application.UseCases.Auctions
{
    public class StartAuction : IStartAuction
    {
        private readonly IAuctionRepository _repo;
        public StartAuction(IAuctionRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<AuctionDTO, Error>> Handle(Guid vehicleId)
        {
            var auctionResult = _repo.GetAuctionByVehicleId(vehicleId);
            if (auctionResult != null) // auction already started
            {
                return Result<AuctionDTO, Error>.Failure(AuctionErrors.AlreadyStarted);
            }

            var auction = new Auction(vehicleId);
            await _repo.StartAuction(auction);

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
