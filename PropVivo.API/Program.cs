using Microsoft.EntityFrameworkCore;
using PropVivo.API;
using PropVivo.API.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddDbContext<EmployeeTaskDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

var startup = new Startup();
startup.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
app.Logger.LogInformation($"Current environment: ${app.Environment.EnvironmentName}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

startup.Configure(app);
app.Run();