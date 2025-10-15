using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Auctions
{
    public class Auction
    {
        public Auction(Guid vehicleId, float currentBid = 0)
        {
            if (currentBid < 0)
            {
                throw new Exception("bid can't be lower than zero.");
            }

            VehicleId = vehicleId;
            CurrentBid = currentBid;
            Status = AuctionStatus.OPEN;
        }

        public Guid VehicleId {  get; set; }

        [Range(0, float.MaxValue)]
        public float CurrentBid { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public AuctionStatus Status { get; set; }

        public bool BidOnAuction(float bid)
        {
            if (bid < 0)
            {
                throw new Exception("Bid can't be a negative value.");
            }
            if (CurrentBid < bid)
            {
                CurrentBid = bid;
                return true;
            }

            return false;
        }

        public override bool Equals(object? obj)
        {
            return obj is Auction auction &&
                   VehicleId.Equals(auction.VehicleId) &&
                   CurrentBid == auction.CurrentBid &&
                   Status == auction.Status;
        }
    }

    public enum AuctionStatus : byte
    {
        OPEN,
        CLOSED,
        // REOPENED
    }
}
