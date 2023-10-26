using Application;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

//builder.Services.AddDistributedMemoryCache(); //InMemory cal�s�yor.
builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration="localhost:6379"); // Redis Cache -> nerde olacak ?

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if(app.Environment.IsProduction()) //E�er app'nin Environment'� IsProduction ise bunu cals�t�r. Developmentta bana son kullan�c� muamel�s� yapma detaylar� �le sistem hatas� sekl�nde var. Production'� ge�ilir ise middleware'i devreye sok.
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
