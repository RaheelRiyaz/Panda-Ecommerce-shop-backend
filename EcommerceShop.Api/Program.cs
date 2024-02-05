using EcommerceShop.Api;
using EcommerceShop.Application;
using EcommerceShop.Infrastructure;
using EcommerceShop.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.
AddPersistenceServices
(builder.Configuration).
AddApiServices(builder.Configuration).
AddApplicationServices(builder.Environment.WebRootPath).
AddInfrastructureServices(builder.Configuration);
var app = builder.Build();
app.UseStaticFiles();
app.UseCors(option =>
{
    option.SetIsOriginAllowed(_ => true)
    .AllowAnyHeader()
    .AllowAnyMethod();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
