using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProductDemo.DAL;
using ProductDemo.DAL.Repositories;
using ProductDemo.Features;
using ProductDemo.Handlers;
using ProductDemo.Mappings;
using ProductDemo.Middleware;
using ProductDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add HttpContextAccessor for Correlation-ID propagation
builder.Services.AddHttpContextAccessor();

// Data Context 
builder.Services.AddDbContext<ProductDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("ProductDatabase")), ServiceLifetime.Singleton);

// Mediatr
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Register pipeline behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

// Dependencies
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register DelegatingHandler
builder.Services.AddTransient<CorrelationIdDelegatingHandler>();

builder.Services.AddHttpClient<IInventoryService, InventoryService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("InventoryProviderUrl"));
}).AddHttpMessageHandler<CorrelationIdDelegatingHandler>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Correlation-ID Middleware (should be early in the pipeline)
app.UseMiddleware<CorrelationIdMiddleware>();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = 400;

            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            await context.Response.WriteAsJsonAsync(new { errors });
        }
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
