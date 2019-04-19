using System.IO;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using IMS.WebSystemKingHub.Logger;

namespace IMS.WebSystemKingHub
{
    public class Program
    {
        public static void Main(string[] args)
        {           
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options =>
                {
                    var ip = string.IsNullOrEmpty(AppSettings.Current.Server.IPAddress) ? IPAddress.Any : IPAddress.Parse(AppSettings.Current.Server.IPAddress);
                    options.Listen(ip, AppSettings.Current.Server.Port);
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddFile(opts =>
                    {
                        context.Configuration.GetSection("FileLoggingOptions").Bind(opts);
                    });
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    var settings = new AppSettings();
                    builder.Build().Bind(settings);

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .UseIISIntegration()
                .UseStartup<Startup>();
    }
}
