using System.Text;
using System.Text.Json.Serialization;
using FinaControl;
using FinaControl.Data;
using FinaControl.Models;
using FinaControl.Repositories;
using FinaControl.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

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
builder.Services.AddTransient<TokenService>();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Fina Control",
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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
