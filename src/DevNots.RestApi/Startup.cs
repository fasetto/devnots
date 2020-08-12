using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DevNots.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DevNots.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // var appConfig = ReadAppConfig();
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STR");

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Environment variable 'CONNECTION_STR' is not set. \nExiting..");
                Environment.Exit(0);
            }

            // services.AddSingleton(appConfig);
            services.AddApplicationDependencies();
            services.AddMongoDb(connectionString);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public AppConfig ReadAppConfig()
        {
            var path = "./appConfig.json";

            #if DEBUG
                path = "../../appConfig.json";
            #endif

            var appConfigJson = File.ReadAllText(path);
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<AppConfig>(appConfigJson, serializerOptions);
        }
    }
}
