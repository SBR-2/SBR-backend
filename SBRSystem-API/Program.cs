using Microsoft.EntityFrameworkCore;
using SBRSystem_API.Extensions;
using SBRSystem_API.GraphQl;
using SBRSystem_Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// builder.Services.AddControllers();
builder.Services.ConfigurarLogger();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<MySBRDbContext>(options => options.UseNpgsql("Host=207.246.81.247;Database=sbrdb;Port=5432;Username=sbradmin;Password=sbr1234;"));




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
