using Domain.Entities.Auctions;

namespace AuctionTest.Domain
{
    [TestClass]
    public class DomainAuctionTest
    {
        #region Auction Creation
        [TestMethod]
        public void AuctionCreation() { 
            var vehicleId = new Guid();
            var auction = new Auction(vehicleId);
            Assert.AreEqual(vehicleId, auction.VehicleId);
            Assert.AreEqual(AuctionStatus.OPEN, auction.Status);
            Assert.AreEqual(0, auction.CurrentBid);

            Assert.ThrowsException<Exception>(() => new Auction(vehicleId, -3));
        }
        #endregion

        #region Bid on Auction
        [TestMethod]
        public void AuctionBidOnAuction()
        {
            var vehicleId = new Guid();
            var auction = new Auction(vehicleId);
            Assert.AreEqual(0, auction.CurrentBid);

            Assert.IsTrue(auction.BidOnAuction(10));
            Assert.AreEqual(10, auction.CurrentBid);
            Assert.IsFalse(auction.BidOnAuction(9));
            Assert.ThrowsException<Exception>(() => auction.BidOnAuction(-9));
        }
        #endregion
    }
}
