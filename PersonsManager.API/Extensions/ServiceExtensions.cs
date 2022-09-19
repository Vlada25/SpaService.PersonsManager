using MassTransit;
using Microsoft.EntityFrameworkCore;
using PersonsManager.API.Services;
using PersonsManager.Database;
using PersonsManager.Interfaces;
using PersonsManager.Interfaces.Services;
using PersonsManager.Messaging.Consumers;
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

            services.AddScoped<ClientChangedSender>();
            services.AddScoped<MasterChangedSender>();
        }

        public static void ConfigureMessageBroker(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureMessageBroker(configuration, consumersConfig => 
            { 
                consumersConfig.AddConsumer<UserClientCreatedConsumer>();
                consumersConfig.AddConsumer<UserClientDeletedConsumer>();
                consumersConfig.AddConsumer<UserMasterCreatedConsumer>();
                consumersConfig.AddConsumer<UserMasterDeletedConsumer>();
            });
        }
    }
}
