namespace Application.Errors
{
    public sealed record Error(string Code, string? Descripton = null)
    {
        public static readonly Error None = new(string.Empty);
    }
    public static class AuctionErrors
    {
        public static readonly Error AlreadyStarted = new("Auction.AlreadyStarted", "This auction has already been started.");
        public static readonly Error NotFound = new("Auction.NotFound", "Auction with the given vehicleId wasn't found.");
        public static readonly Error AlreadyClosed = new("Auction.AlreadyClosed", "Trying to close an already closed auction.");
        public static readonly Error AuctionClosed = new("Auction.AuctionClosed", "Trying to place a bid on a closed auction.");
        public static readonly Error SmallerBid = new("Auction.SmallerBid", "Placed bid is smaller than current bid.");
        public static readonly Error FailedToUpdate = new("Auction.FailedToUpdate", "Failed to update auction.");
    }

    public static class VehicleErrors
    {
        public static readonly Error FailedToCreate = new("Vehicle.FailedToCreate", "Failed to create vehicle.");
        public static readonly Error NotFound = new("Vehicle.NotFound", "Vehicle was not found.");
    }

    
}