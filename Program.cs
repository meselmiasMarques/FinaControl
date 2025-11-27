using FinaControl.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FinaControlDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();


app.Run();
