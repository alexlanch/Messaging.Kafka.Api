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


namespace Messaging.Kafka.Infrastructure.Services.Background
{
    public class Worker : BackgroundService
    {
        private readonly string _connectionString = string.Empty;
        private readonly ILogger<Worker> _logger;
        private readonly LoggingContext _context;

        public Worker(ILogger<Worker> logger, LoggingContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _connectionString = configuration.GetValue<string>("ConnectionStringAccessControl") ?? string.Empty;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var connectionSql = new SqlConnection(_connectionString))
                while (!stoppingToken.IsCancellationRequested)
                {
                    var logMessage = $"Worker running at: {DateTimeOffset.Now}";
                    _logger.LogInformation(logMessage);

                    // Insertar en la base de datos

                    var objLogsEntry = new objLogsEntry
                    {
                        Timestamp = DateTime.Now,

                    };
                    _context.LogsEntry.Add(objLogsEntry);
                    await _context.SaveChangesAsync(stoppingToken);

                    await Task.Delay(30000, stoppingToken);
                }
        }
    }
}
