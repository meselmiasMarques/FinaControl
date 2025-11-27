using FinaControl.Data;
using FinaControl.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FinaControlDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddTransient<UserRepository>();

var app = builder.Build();
app.MapControllers();

app.Run();
