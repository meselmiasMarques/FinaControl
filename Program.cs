using System.Text.Json.Serialization;
using FinaControl.Data;
using FinaControl.Models;
using FinaControl.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => 
        { options.SuppressModelStateInvalidFilter = true; }).AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; //ignora cicos de cadeias
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FinaControlDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


//INJEÇÃO DE DEPENDENCIAS
builder.Services.AddTransient<TransactionRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<CategoryRepository>();
builder.Services.AddTransient<RoleRepository>();



var app = builder.Build();
app.MapControllers();

app.Run();
