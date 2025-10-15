using Domain.Entities.Auctions;
using Persistence.Repositories;

namespace AuctionTest.Repositories
{
    [TestClass]
    public class RepoAuctionTest
    {
        private AuctionRepository _repo = new AuctionRepository();

        [TestMethod]
        public void StartAuction()
        {
            var vehicleId = Guid.NewGuid();
            var auction = new Auction(vehicleId);
            var result = _repo.StartAuction(auction).Result;
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateAuctionSuccess()
        {
            var vehicleId = Guid.NewGuid();
            var auction = new Auction(vehicleId);
            var result = _repo.StartAuction(auction).Result;
            var oldBid = auction.CurrentBid;
            Assert.IsTrue(result);

            auction.CurrentBid = 1000;
            await _repo.UpdateAuction(auction);
            var getAuction = await _repo.GetAuctionByVehicleId(vehicleId)!;
            Assert.IsNotNull(getAuction);
            Assert.IsFalse(oldBid == getAuction.CurrentBid);
        }

        [TestMethod]
        public async Task UpdateAuctionFailure()
        {
            var vehicleId = Guid.NewGuid();
            var auction = new Auction(vehicleId);
            await _repo.UpdateAuction(auction);
            var getAuction = _repo.GetAuctionByVehicleId(vehicleId)?.Result;
            Assert.IsNull(getAuction);
        }

        [TestMethod]
        public async Task GetAuctionByVehicleIdSuccess()
        {
            var vehicleId = Guid.NewGuid();
            var auction = new Auction(vehicleId);
            var result = _repo.StartAuction(auction).Result;
            Assert.IsTrue(result);
            var getAuction = await _repo.GetAuctionByVehicleId(vehicleId);
            Assert.IsNotNull(getAuction);
            Assert.AreEqual(auction, getAuction);
        }

        [TestMethod]
        public async Task GetAuctionByVehicleIdFailure()
        {
            var getAuction = _repo.GetAuctionByVehicleId(Guid.NewGuid())?.Result;
            Assert.IsNull(getAuction);
        }
    }
}