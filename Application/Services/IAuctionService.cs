using Application.DTO;
using Application.Errors;

namespace Application.Services
{
    public interface IAuctionService
    {
        Task<Result<AuctionDTO, Error>> StartAuction(Guid vehicleId);
        Task<Result<AuctionDTO, Error>> CloseAuction(Guid vehicleId);
        Task<Result<AuctionDTO, Error>> PlaceBidOnAuction(Guid vehicleId, float bid);
    }
}
