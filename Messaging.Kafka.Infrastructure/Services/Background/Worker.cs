using Messaging.Kafka.Infrastructure.Data.Persitence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Messaging.Kafka.Infrastructure.Data.Persintence;
using Microsoft.Extensions.Hosting;
using Confluent.Kafka;
using Microsoft.Extensions.Options;


namespace Messaging.Kafka.Infrastructure.Services.Background
{
    public class Worker : BackgroundService
    {
        private readonly string _connectionString = string.Empty;
        private readonly ILogger<Worker> _logger;
        private readonly LoggingContext _context;
        private readonly IConsumer<Null, string> _consumer;

        public Worker(ILogger<Worker> logger, LoggingContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _connectionString = configuration.GetSection("ConnectionStrings")["ConnectionStringAccessControl"];

            // Configuración del consumidor de Kafka
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "DESKTOP-2S6R7FK:9091, DESKTOP-2S6R7FK:9092, DESKTOP-2S6R7FK:9093",
                GroupId = "sensors-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("tracking.sensors.networklistenertest");  // Suscribirse al topic de Kafka

            using (var connectionSql = new SqlConnection(_connectionString))
            {
                await connectionSql.OpenAsync(stoppingToken);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var logMessage = $"Worker running at: {DateTimeOffset.Now}";
                    _logger.LogInformation(logMessage);

                    try
                    {
                        // Consumir mensajes de Kafka
                        var consumeResult = _consumer.Consume(stoppingToken);
                        var kafkaMessage = consumeResult.Message.Value;

                        // Registrar en la base de datos el mensaje consumido
                        var objLogsEntry = new objLogsEntry
                        {
                            Timestamp = DateTime.Now,
                            Message = kafkaMessage  // Asumiendo que objLogsEntry tiene un campo para almacenar el mensaje
                        };

                        _context.LogsEntry.Add(objLogsEntry);
                        await _context.SaveChangesAsync(stoppingToken);

                        _logger.LogInformation($"Mensaje consumido e insertado en la base de datos: {kafkaMessage}");
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError($"Error al consumir mensaje: {e.Error.Reason}");
                    }

                    // Espera de 30 segundos antes de consumir el siguiente mensaje
                    await Task.Delay(30000, stoppingToken);
                }
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }

    }
}
