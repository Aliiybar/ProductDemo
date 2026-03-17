using Microsoft.EntityFrameworkCore;
using ProductDemo.DAL;
using ProductDemo.DAL.Repositories;
using ProductDemo.Mappings;

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

// Dependencies
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ApplicationProfile>();
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
