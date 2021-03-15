using Microsoft.Extensions.DependencyInjection;
using Vonk.Core.Pluggability;
using Microsoft.AspNetCore.Builder;

namespace CsvFrontend
{

    [VonkConfiguration(order: 800)]
    public static class VPlugin
    {

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSpaStaticFiles(options => options.RootPath = "plugins/webapplication/csv-serialiser/dist");
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseHsts();
            app.UseRouting();
            app.UseSpaStaticFiles();

            app.Map("/csv-serialiser", builder =>
            {
                builder.UseSpaStaticFiles();
                builder.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "plugins/webapplication/csv-serialiser";
                });
            });
        }

    }
}
