using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OTPResetAPI
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
            // CORS Setting
            var allowedDomains = this.Configuration.GetSection("CorsSettings:AllowOrigins").Get<string[]>();
            var allowedMethods = this.Configuration.GetSection("CorsSettings:AllowMethods").Get<string[]>();
            var allowedHeaders = this.Configuration.GetSection("CorsSettings:AllowHeaders").Get<string[]>();
            services.AddCors(
                o => o.AddPolicy(
                    "CorsAllowAny",
                    b =>
                    {
                        b.WithOrigins(allowedDomains)
                             .WithMethods(allowedMethods)
                             .WithHeaders(allowedHeaders)
                             //.AllowCredentials()
                             .AllowAnyOrigin()
                             ;
                    }));
            // CORS Setting
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsAllowAny");    // CORS
            app.UseHttpsRedirection();  //HTTPS

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
