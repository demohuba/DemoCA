using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace JEPCO.Shared.Extensions
{
    public static class StartupExtension
    {
        private static IServiceCollection _services { get; set; }

        public static void RegisterSharedServices(this IServiceCollection services, IConfiguration config)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));

            #region localization
            _services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Configure supported cultures
            var supportedCultures = new[]
            {
        new CultureInfo("ar"),
        new CultureInfo("en"),
        };
            foreach (var culture in supportedCultures)
            {
                culture.NumberFormat = NumberFormatInfo.InvariantInfo;
                culture.DateTimeFormat = DateTimeFormatInfo.InvariantInfo;
            }

            _services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            #endregion

            // mapper
            _services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        }


        public static void AddSharedMiddlewares(this IApplicationBuilder app)
        {

            // Enable localization middleware
            app.UseRequestLocalization();
        }
    }
}
