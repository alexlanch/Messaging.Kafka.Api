using Messaging.Kafka.Infrastructure.Data.Persintence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Kafka.Infrastructure.Data.Persitence
{
    public class LoggingContext : DbContext
    {
        public LoggingContext(DbContextOptions<LoggingContext> options) : base(options) { }

        public DbSet<objLogsEntry> LogsEntry { get; set; }
    }
}
