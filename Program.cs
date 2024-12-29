using Hotel_Management_API.Data.DBContexts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//jwt
var jwtIssuer = builder.Configuration.GetSection("JWTSettings:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("JWTSettings:Key").Get<string>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });
//end of jwt 

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

//Inject services    
//builder.Services.AddScoped<IEmployeeService, EmployeeService>();
//builder.Services.AddHttpClient<IPositionClient, PositionClient>();
//builder.Services.AddHttpClient<IIdentityClient, IdentityClient>();

builder.Services.AddDbContext<HotelDBContext>(options =>
{
    Log.Information($"Using {builder.Environment.EnvironmentName} DB");
    var connectionString = builder.Configuration.GetConnectionString("DbConnection");
    //if (builder.Environment.IsDevelopment())
    //{
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    //}
    //else
    //{
    //    connectionString = builder.Configuration.GetConnectionString("SQLConnection");
    //    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    //}
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.UseAuthentication();    
//app.UseMiddleware<ErrorHandlingMiddleware>();

//if (builder.Environment.IsProduction())
//{
//    Log.Information("DI for context in production");
//    var db = app.Services.GetRequiredService<HotelDBContext>();
//    await db.Database.MigrateAsync();

//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
