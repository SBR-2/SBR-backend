using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl;
using SBRSystem_Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<MySBRDbContext>(options => options.UseNpgsql("Host=207.246.81.247;Database=sbrdb;Port=5432;Username=sbradmin;Password=sbr1234;"));

//Agregar servicios para autenticacion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "SBRSystem",
        ValidAudience = "APIUsers",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aVeryLongSecretKeyForJWTTokenThatIsAtLeast32Chars")),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});


builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddAuthorization() 
    .AddMutationConventions()
    .RegisterDbContext<MySBRDbContext>();


var app = builder.Build();



// Middleware de autenticación y autorización
app.UseAuthentication(); // Agregar autenticación JWT
app.UseAuthorization();  // Agregar autorización

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


// app.MapControllers();
app.MapGraphQL("/graphql");
app.Run();
