using Application.DTO;
using Application.Errors;
using Application.UseCases.Auctions;

namespace Application.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IStartAuction startAuctionUseCase;
        private readonly ICloseAuction closeAuctionUseCase;
        private readonly IPlaceBidOnAuction placeBiOnAuctionUseCase;

        public AuctionService(IStartAuction start, ICloseAuction close, IPlaceBidOnAuction placeBid)
        {
            startAuctionUseCase = start;
            closeAuctionUseCase = close;
            placeBiOnAuctionUseCase = placeBid;
        }

        public Task<Result<AuctionDTO, Error>> StartAuction(Guid vehicleId)
        {
            return startAuctionUseCase.Handle(vehicleId);
        }

        public Task<Result<AuctionDTO, Error>> CloseAuction(Guid vehicleId)
        {
            return closeAuctionUseCase.Handle(vehicleId);
        }

        public Task<Result<AuctionDTO, Error>> PlaceBidOnAuction(Guid vehicleId, float bid)
        {
            return placeBiOnAuctionUseCase.Handle(vehicleId, bid);
        }
    }
}