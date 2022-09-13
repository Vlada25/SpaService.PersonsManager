using MassTransit;
using Microsoft.EntityFrameworkCore;
using PersonsManager.API.Services;
using PersonsManager.Database;
using PersonsManager.Interfaces;
using PersonsManager.Interfaces.Services;
using PersonsManager.Messaging.Senders;
using SpaService.Domain.Configuration;

namespace PersonsManager.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<PersonsManagerDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
                    b.MigrationsAssembly("PersonsManager.Database")));
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IMastersService, MastersService>();

            services.AddScoped<ClientDeletedSender>();
            services.AddScoped<ClientUpdatedSender>();
            services.AddScoped<MasterDeletedSender>();
            services.AddScoped<MasterUpdatedSender>();
        }

        public static void ConfigureMessageBroker(this IServiceCollection services,
            IConfiguration configuration)
        {
            var messagingConfig = configuration.GetSection("Messaging");
            services.Configure<MessagingConfig>(messagingConfig);

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messagingConfig["Hostname"], "/", h =>
                    {
                        h.Username(messagingConfig["UserName"]);
                        h.Password(messagingConfig["Password"]);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
