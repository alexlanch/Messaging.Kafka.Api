using Messaging.Kafka.Domain.Interfaces;
using Messaging.Kafka.Domain.Services;
using Messaging.Kafka.Infrastructure;
using Messaging.Kafka.Api.Extensions;
using Microsoft.Extensions.Options;
using Messaging.Kafka.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Messaging.Kafka.Infrastructure.Data.Persitence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddSwaggerExtension(builder.Configuration);

// Agregar servicios Kafka
builder.Services.AddTransient<IKafkaRepository, KafkaRepository>();
builder.Services.AddTransient<IKafkaService, KafkaService>();


var app = builder.Build();

// Configurar Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API v1");
    options.RoutePrefix = string.Empty; // Esto hace que Swagger esté en la raíz
});

// Configuración del pipeline HTTP
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();
