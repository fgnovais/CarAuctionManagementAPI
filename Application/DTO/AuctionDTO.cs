using Domain.Entities.Auctions;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class AuctionDTO
    {
        public Guid VehicleId { get; set; }
        [Range(0, float.MaxValue)]
        public float CurrentBid { get; set; }
        public AuctionStatus Status { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is AuctionDTO dTO &&
                   Status == dTO.Status;
        }
    }
}
