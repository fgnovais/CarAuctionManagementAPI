using Application.DTO;
using Application.Errors;
using Application.UseCases.Auctions;
using Domain.Entities.Auctions;
using Persistence.Repositories;

namespace AuctionTest.Services
{
    [TestClass]
    public class ServiceAuctionTest
    {
        private static AuctionRepository _repo = new AuctionRepository();
        private PlaceBid placeBidUseCase = new PlaceBid(_repo);
        private CloseAuction closeAuctionUseCase = new CloseAuction(_repo);
        private StartAuction startAuctionUseCase = new StartAuction(_repo);

        [TestMethod]
        public async Task StartAuctionSuccess()
        {
            var guid = Guid.NewGuid();
            var result = await startAuctionUseCase.Handle(guid);

            Assert.IsTrue(result.IsSuccess);
            var dto = new AuctionDTO
            {
                VehicleId = guid,
                CurrentBid = 0,
                Status = AuctionStatus.OPEN
            };
            Assert.AreEqual(result._value, dto);
        }

        [TestMethod]
        public async Task StartAuctionFailure()
        {
            var guid = Guid.NewGuid();
            await startAuctionUseCase.Handle(guid);
            var result = await startAuctionUseCase.Handle(guid);

            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result._error, AuctionErrors.AlreadyStarted);
        }

        [TestMethod]
        public async Task CloseAuctionSuccess()
        {
            var guid = Guid.NewGuid();
            await startAuctionUseCase.Handle(guid);
            var result = await closeAuctionUseCase.Handle(guid);

            Assert.IsTrue(result.IsSuccess);
            var dto = new AuctionDTO
            {
                VehicleId = guid,
                CurrentBid = 0,
                Status = AuctionStatus.CLOSED
            };
            Assert.AreEqual(result._value, dto);
        }


        [TestMethod]
        public async Task CloseAuctionFailure()
        {
            var guid = Guid.NewGuid();
            var result = await closeAuctionUseCase.Handle(guid);

            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result._error, AuctionErrors.NotFound);

            await startAuctionUseCase.Handle(guid);
            await closeAuctionUseCase.Handle(guid);
            result = await closeAuctionUseCase.Handle(guid);

            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result._error, AuctionErrors.AlreadyClosed);
        }

        [TestMethod]
        public async Task StartAuctionAfterClosing()
        {
            var guid = Guid.NewGuid();
            await startAuctionUseCase.Handle(guid);
            await closeAuctionUseCase.Handle(guid);
            var result = await startAuctionUseCase.Handle(guid);

            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result._error, AuctionErrors.AlreadyStarted);
        }

        [TestMethod]
        public async Task PlaceBidSuccess()
        {
            var guid = Guid.NewGuid();
            var result = await startAuctionUseCase.Handle(guid);
            Assert.IsTrue(result.IsSuccess);

            result = await placeBidUseCase.Handle(guid, 100);
            Assert.IsTrue(result.IsSuccess);

            var auction = _repo.GetAuctionByVehicleId(guid)?.Result;
            Assert.IsNotNull(auction);
            Assert.AreEqual(100, auction.CurrentBid);
        }

        [TestMethod]
        public async Task PlaceBidFailure()
        {
            var guid = Guid.NewGuid();
            var result = await placeBidUseCase.Handle(guid, 100);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result._error, AuctionErrors.NotFound);

            await startAuctionUseCase.Handle(guid);
            result = await placeBidUseCase.Handle(guid, 100);
            Assert.IsTrue(result.IsSuccess);

            result = await placeBidUseCase.Handle(guid, 50);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result._error, AuctionErrors.SmallerBid);

            await closeAuctionUseCase.Handle(guid);
            result = await placeBidUseCase.Handle(guid, 150);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result._error, AuctionErrors.AuctionClosed);
        }
    }
}
