using Messaging.Kafka.Infrastructure.Services.Background;

namespace Messaging.Kafka.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Registrar la configuración
            services.Configure<AppSettings>(Configuration.GetSection("MyAppSettings"));

            // Registrar otros servicios necesarios para la aplicación
            services.AddHostedService<Worker>(); // Registro del Worker
        }
    }
}
