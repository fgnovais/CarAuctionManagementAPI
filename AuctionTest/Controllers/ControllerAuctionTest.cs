using Application.DTO;
using Application.Errors;
using Application.Services;
using Domain.Entities.Auctions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Web.API;


namespace AuctionTest.Controllers
{
    [TestClass]
    public class ControllerAuctionTest
    {
        #region Success
        public class MockAuctionServiceSuccess : IAuctionService
        {
            public async Task<Result<AuctionDTO, Error>> CloseAuction(Guid vehicleId)
            {
                var mappedAuction = new AuctionDTO
                {
                    VehicleId = new Guid("a03319be-0f17-452b-b451-eb4206840f59"),
                    CurrentBid = 100,
                    Status = AuctionStatus.CLOSED,
                };
                return Result<AuctionDTO, Error>.Success(mappedAuction);
            }

            public async Task<Result<AuctionDTO, Error>> PlaceBidOnAuction(Guid vehicleId, float bid)
            {
                var mappedAuction = new AuctionDTO
                {
                    VehicleId = new Guid("a03319be-0f17-452b-b451-eb4206840f59"),
                    CurrentBid = 150,
                    Status = AuctionStatus.OPEN,
                };
                return Result<AuctionDTO, Error>.Success(mappedAuction);
            }

            public async Task<Result<AuctionDTO, Error>> StartAuction(Guid vehicleId)
            {
                var mappedAuction = new AuctionDTO
                {
                    VehicleId = new Guid("a03319be-0f17-452b-b451-eb4206840f59"),
                    CurrentBid = 100,
                    Status = AuctionStatus.OPEN,
                };
                return Result<AuctionDTO, Error>.Success(mappedAuction);
            }
        }

        [TestMethod]
        public async Task StartAuctionSuccess()
        {
            await using var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(static builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IAuctionService, MockAuctionServiceSuccess>();
                });
            });

            using var client = factory.CreateClient();
            var content = new StringContent("a03319be-0f17-452b-b451-eb4206840f59", Encoding.UTF8, "text/plain");
            var response = await client.PatchAsync("api/Auction/start", content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("{\"vehicleId\":\"a03319be-0f17-452b-b451-eb4206840f59\",\"currentBid\":100,\"status\":\"OPEN\"}", result);
        }

        [TestMethod]
        public async Task CloseAuctionSuccess()
        {
            await using var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(static builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IAuctionService, MockAuctionServiceSuccess>();
                });
            });

            using var client = factory.CreateClient();
            var content = new StringContent("a03319be-0f17-452b-b451-eb4206840f59", Encoding.UTF8, "text/plain");
            var response = await client.PatchAsync("api/Auction/close", content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("{\"vehicleId\":\"a03319be-0f17-452b-b451-eb4206840f59\",\"currentBid\":100,\"status\":\"CLOSED\"}", result);
        }

        [TestMethod]
        public async Task PlaceBidOnAuctionSuccess()
        {
            await using var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(static builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IAuctionService, MockAuctionServiceSuccess>();
                });
            });

            using var client = factory.CreateClient();
            var request = new PlaceBidDTO
            {
                VehicleId = new Guid("a03319be-0f17-452b-b451-eb4206840f59"),
                Bid = 150
            };
            var response = await client.PatchAsJsonAsync("api/Auction/bid", request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("{\"vehicleId\":\"a03319be-0f17-452b-b451-eb4206840f59\",\"currentBid\":150,\"status\":\"OPEN\"}", result);
        }
        #endregion

        #region Failure
        public class MockAuctionServiceFailure : IAuctionService
        {
            public async Task<Result<AuctionDTO, Error>> CloseAuction(Guid vehicleId)
            {
                return Result<AuctionDTO, Error>.Failure(AuctionErrors.AlreadyClosed);
            }

            public async Task<Result<AuctionDTO, Error>> PlaceBidOnAuction(Guid vehicleId, float bid)
            {
                return Result<AuctionDTO, Error>.Failure(AuctionErrors.FailedToUpdate);
            }

            public async Task<Result<AuctionDTO, Error>> StartAuction(Guid vehicleId)
            {
                return Result<AuctionDTO, Error>.Failure(AuctionErrors.AlreadyStarted);
            }
        }

        [TestMethod]
        public async Task StartAuctionFailure()
        {
            await using var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(static builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IAuctionService, MockAuctionServiceFailure>();
                });
            });

            using var client = factory.CreateClient();
            var content = new StringContent("a03319be-0f17-452b-b451-eb4206840f59", Encoding.UTF8, "text/plain");
            var response = await client.PatchAsync("api/Auction/start", content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("{\"code\":\"Auction.AlreadyStarted\",\"descripton\":\"This auction has already been started.\"}", result);
        }

        [TestMethod]
        public async Task CloseAuctionFailure()
        {
            await using var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(static builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IAuctionService, MockAuctionServiceFailure>();
                });
            });

            using var client = factory.CreateClient();
            var content = new StringContent("a03319be-0f17-452b-b451-eb4206840f59", Encoding.UTF8, "text/plain");
            var response = await client.PatchAsync("api/Auction/close", content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("{\"code\":\"Auction.AlreadyClosed\",\"descripton\":\"Trying to close an already closed auction.\"}", result);
        }

        [TestMethod]
        public async Task PlaceBidOnAuctionFailure()
        {
            await using var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(static builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IAuctionService, MockAuctionServiceFailure>();
                });
            });

            using var client = factory.CreateClient();
            var request = new PlaceBidDTO
            {
                VehicleId = new Guid("a03319be-0f17-452b-b451-eb4206840f59"),
                Bid = 100
            };
            var response = await client.PatchAsJsonAsync("api/Auction/bid", request);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("{\"code\":\"Auction.FailedToUpdate\",\"descripton\":\"Failed to update auction.\"}", result);
        }
        #endregion


    }
}
