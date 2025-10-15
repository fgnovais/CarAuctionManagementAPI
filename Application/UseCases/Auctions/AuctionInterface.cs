using Application.DTO;
using Application.Errors;

namespace Application.UseCases.Auctions
{
    public interface IStartAuction
    {
        Task<Result<AuctionDTO, Error>> Handle(Guid vehicleId);
    }
    public interface ICloseAuction
    {
        Task<Result<AuctionDTO, Error>> Handle(Guid vehicleId);
    }

    public interface IPlaceBidOnAuction
    {
        Task<Result<AuctionDTO, Error>> Handle(Guid vehicleId, float bid);
    }
}
