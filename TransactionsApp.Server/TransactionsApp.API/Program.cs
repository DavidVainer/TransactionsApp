using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using System.Text.Json.Serialization;
using TransactionsApp.API.IoC;
using TransactionsApp.API.Middlewares;
using TransactionsApp.Application.Services.Implementations.Validators;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Replace default logging with Serilog
builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<AddTransactionDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTransactionDtoValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDI(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost8080", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("http://localhost:8080");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors("AllowLocalhost8080");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionsHandlingMiddleware>();

app.Run();
