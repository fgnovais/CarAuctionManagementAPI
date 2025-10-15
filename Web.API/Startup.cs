using Application.Services;
using Application.UseCases.Auctions;
using Application.UseCases.Vehicles;
using Domain.Repositories;
using Microsoft.OpenApi.Models;
using Persistence.Repositories;
using Web.API.Middleware;

namespace Web.API;

public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebApp", Version = "v1" });
        });

        services.AddScoped<IVehicleService, VehicleService>();

        services.AddControllers()
        .AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        }); // show enum names in swagger

        // repos
        services.AddSingleton<IVehicleRepository, VehicleRepository>();
        services.AddSingleton<IAuctionRepository, AuctionRepository>();
        // services
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IAuctionService, AuctionService>();
        // use cases
        services.AddScoped<IStartAuction, StartAuction>();
        services.AddScoped<ICloseAuction, CloseAuction>();
        services.AddScoped<IPlaceBidOnAuction, PlaceBid>();
        services.AddScoped<IAddVehicle, AddVehicle>();
        services.AddScoped<IGetFilteredVehicles, FilterVehicle>();
        services.AddScoped<IPlaceBidOnAuction, PlaceBid>();
        services.AddScoped<IAuctionService, AuctionService>();
        //exception handler
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}