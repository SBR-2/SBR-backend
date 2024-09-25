using SBRSystem_API.GraphQl;
using SBRSystem_Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MySBRDbContext>();




builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections()
    .RegisterDbContextFactory<MySBRDbContext>();
    

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
