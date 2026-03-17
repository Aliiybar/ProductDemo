using Microsoft.EntityFrameworkCore;
using ProductDemo.DAL;
using ProductDemo.DAL.Repositories;
using ProductDemo.Mappings;
using ProductDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Data Context 
builder.Services.AddDbContext<ProductDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("ProductDatabase")), ServiceLifetime.Singleton);

// Mediatr
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ApplicationProfile>();
});

// Dependencies
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddHttpClient<IInventoryService, InventoryService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("InventoryProviderUrl"));
});

var app = builder.Build();

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
