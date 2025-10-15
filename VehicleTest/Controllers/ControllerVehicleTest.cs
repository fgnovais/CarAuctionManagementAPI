using Application.DTO;
using Application.Errors;
using Application.Services;
using Domain.Entities.Vehicles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Web.API;

namespace VehicleTest.Controllers
{
    [TestClass]
    public class ControllerVehicleTest
    {
        #region Success

        public class MockVehicleServiceSuccess : IVehicleService
        {
            public async Task<Result<VehicleDTO, Error>> AddVehicle(Vehicle vehicle)
            {
                var mappedVehicle = new VehicleDTO
                {
                    Id = new Guid("a03319be-0f17-452b-b451-eb4206840f59"),
                    Manufacturer = vehicle.Manufacturer,
                    Model = vehicle.Model,
                    Year = vehicle.Year,
                    StartingBid = vehicle.StartingBid,
                    Type = vehicle.Type,
                };
                return Result<VehicleDTO, Error>.Success(mappedVehicle);
            }

            public async Task<Result<List<VehicleDTO>, Error>> GetFilteredVehicles(VehicleFilter filter)
            {
                var mappedVehicle = new VehicleDTO
                {
                    Id = new Guid("a9bf60e6-3bc3-4cf1-aeb0-3983c9398d5c"),
                    Manufacturer = "Seat",
                    Model = "Ibiza",
                    Year = 1995,
                    StartingBid = 0,
                    Type = VehicleType.HATCHBACK,
                };
                var list = new List<VehicleDTO>();
                list.Add(mappedVehicle);
                return Result<List<VehicleDTO>, Error>.Success(list);
            }
        }

        [TestMethod]
        public async Task GetVehiclesSuccess()
        {
            await using var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(static builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IVehicleService, MockVehicleServiceSuccess>();
                });
            });

            using var client = factory.CreateClient();
            var response = await client.GetAsync("api/Vehicle/");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("[{\"id\":\"a9bf60e6-3bc3-4cf1-aeb0-3983c9398d5c\",\"manufacturer\":\"Seat\",\"model\":\"Ibiza\",\"startingBid\":0,\"year\":1995,\"type\":\"HATCHBACK\"}]", result);
        }

        [TestMethod]
        public async Task AddVehicleSuccess()
        {
            await using var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(static builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IVehicleService, MockVehicleServiceSuccess>();
                });
            });

            using var client = factory.CreateClient();
            var json = @"{
                  ""$type"": ""SUV"",
                  ""manufacturer"": ""Tesla"",
                  ""model"": ""Super"",
                  ""year"": 2023,
                  ""startingBid"": 20000,
                  ""NumberOfSeats"": 5
                }";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Vehicle/create", content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("{\"id\":\"a03319be-0f17-452b-b451-eb4206840f59\",\"manufacturer\":\"Tesla\",\"model\":\"Super\",\"startingBid\":20000,\"year\":2023,\"type\":\"SUV\"}", result);
        }

        #endregion

        #region Failure

        public class MockVehicleServiceFailure : IVehicleService
        {
            public async Task<Result<VehicleDTO, Error>> AddVehicle(Vehicle vehicle)
            {
                return Result<VehicleDTO, Error>.Failure(VehicleErrors.FailedToCreate);
            }

            public async Task<Result<List<VehicleDTO>, Error>> GetFilteredVehicles(VehicleFilter filter)
            {
                return Result<List<VehicleDTO>, Error>.Failure(VehicleErrors.NotFound);
            }
        }
        [TestMethod]
        public async Task GetVehiclesFailure()
        {
            await using var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(static builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IVehicleService, MockVehicleServiceFailure>();
                });
            });

            using var client = factory.CreateClient();
            var response = await client.GetAsync("api/Vehicle/");

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("{\"code\":\"Vehicle.NotFound\",\"descripton\":\"Vehicle was not found.\"}", result);
        }

        [TestMethod]
        public async Task AddVehiclesFailure()
        {
            await using var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(static builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IVehicleService, MockVehicleServiceFailure>();
                });
            });

            using var client = factory.CreateClient();
            var json = @"{
                  ""$type"": ""SUV"",
                  ""manufacturer"": ""Seat"",
                  ""model"": ""Ibiza"",
                  ""year"": 2003,
                  ""startingBid"": 2,
                  ""NumberOfSeats"": 4
                }";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Vehicle/create", content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("{\"code\":\"Vehicle.FailedToCreate\",\"descripton\":\"Failed to create vehicle.\"}", result);
        }

        #endregion
    }
}
