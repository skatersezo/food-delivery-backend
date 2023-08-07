using Microsoft.EntityFrameworkCore;
using FoodDelivery.Core.Adaptors.Db;
using Paramore.Brighter.Extensions.DependencyInjection;
using Paramore.Darker.AspNetCore;
using FoodDelivery.Core.Ports.Queries;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FoodDeliveryDbContext>(options => options.UseNpgsql("Host=localhost;Database=FoodDeliveryDB;Username=postgres;Password=password"));

builder.Services.AddBrighter(options =>
    options.CommandProcessorLifetime = ServiceLifetime.Scoped)
    .AutoFromAssemblies();

builder.Services.AddDarker(options =>
    options.QueryProcessorLifetime = ServiceLifetime.Scoped)
    .AddHandlersFromAssemblies(typeof(DeliveryDriverByIdQuery).Assembly);

builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseCors("AllowAll");

using(var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<FoodDeliveryDbContext>().Database.EnsureCreated();
}

app.MapControllers();

app.Run();

