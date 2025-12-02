using System.Text.Json.Serialization;
using FinaControl.Data;
using FinaControl.Models;
using FinaControl.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

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

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Fina Controle",
        Version = "v1 - Fina Controle",
        Description = "Fina Controle de Acesso"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fina Control Api");
   c.RoutePrefix = string.Empty; // Faz o swagger abrir na raiz "/"

});


app.MapControllers();

app.Run();
