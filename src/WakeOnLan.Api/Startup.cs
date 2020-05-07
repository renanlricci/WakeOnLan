using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WakeOnLan.Api.Infrastruture.Extensions;
using WakeOnLan.CrossCutting.Configuration;
using System.IO;

namespace WakeOnLan.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; set; }

        public Startup()
        {
            _configuration = ConfigurationBuilder();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = _configuration.Get<AppSettings>();

            services.AddMvc();
            services.Configure<AppSettings>(_configuration);
            services
                .AddSwagger(appSettings)
                .AddSwaggerBearerSecurity()
                .AddBearerAuthentication(appSettings)
                .AddIoC();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc().UseSwaggerApplication();
        }

        private static IConfiguration ConfigurationBuilder() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
    }
}
