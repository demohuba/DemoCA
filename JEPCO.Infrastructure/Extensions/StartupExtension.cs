using JEPCO.Application.Interfaces.CacheManagement;
using JEPCO.Application.Interfaces.FileExport;
using JEPCO.Application.Interfaces.IAM;
using JEPCO.Application.Interfaces.UnitOfWork;
//using JEPCO.Infrastructure.CacheManagement;
using JEPCO.Infrastructure.FileExport.CSV;
using JEPCO.Infrastructure.IAM;
using JEPCO.Infrastructure.Persistence;
using JEPCO.Infrastructure.Persistence.DbMigrationAssembly;
using JEPCO.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace JEPCO.Infrastructure.Extensions
{
    public static class StartupExtension
    {
        private static IServiceCollection _services { get; set; }

        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string dbSchema = configuration.GetValue<string>("DBSchema")!;

            //var appConfigurations = configuration.GetSection("AppConfigurations").Get<AppConfigurations>();

            if (string.IsNullOrEmpty(connectionString))
                throw new NotImplementedException("Database default connection string is not recognized");

            //if (string.IsNullOrEmpty(appConfigurations.RedisConnectionString))
            //    throw new NotImplementedException("redis default connection string is not recognized");

            // Register the Redis connection multiplexer as a singleton
            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(appConfigurations.RedisConnectionString));

            AddDatabaseContext(connectionString, dbSchema);
            // _services.AddEntityFrameworkNpgsqlNetTopologySuite();

            _services.AddScoped<IUnitOfWork, UnitOfWork>();
            //_services.AddScoped<ICacheManager, RedisCacheManager>();
            _services.AddSingleton<IIdentityUserManager, KeycloakUserManager>();
            _services.AddScoped<IFileExport, CSVManager>();

        }
        private static void AddDatabaseContext(string connectionString, string dbSchema)
        {
            _services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString, builder =>
                {
                    builder.MigrationsHistoryTable("migrations", dbSchema);
                    builder.UseNetTopologySuite();
                })
                .ReplaceService<IMigrationsAssembly, DbSchemaAwareMigrationAssembly>();
            });
        }


        public static void MigrateDatabaseToLatestVersion(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                Console.WriteLine($"ConnectionString: {context.Database.GetConnectionString()}");
                context.Database.Migrate();
            }
        }
    }
}
