using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WakeOnLan.Api.Infrastruture.Extensions;
using WakeOnLan.CrossCutting.Configuration;

namespace WakeOnLan.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = _configuration.Get<AppSettings>();

            services.AddCors();
            services.AddControllers();
            services.Configure<AppSettings>(_configuration);
            services
                .AddSwagger(appSettings)
                .AddSwaggerBearerSecurity()
                .AddBearerAuthentication(appSettings)
                .AddIoC();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(a => a
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerApplication();
        }
    }
}
