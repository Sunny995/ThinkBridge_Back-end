using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarData.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarData
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

            });
            services.AddDbContext<CarContext>(options =>

           options.UseSqlServer(Configuration.GetConnectionString("CarConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddOpenApiDocument(document =>

            {

                document.PostProcess = process =>

                {

                    process.Info.Version = "v1";

                    process.Info.Title = "Car Booking API";

                    process.Info.Description = "Process Reports";

                    process.Info.TermsOfService = "None";

                    process.Info.Contact = new NSwag.SwaggerContact

                    {

                        Name = "Sunny Kumar",

                        Email = "Sunny.Kumar@Euromonitor.com",

                        Url = "https://www.euromonitor.com"

                    };

                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseSwagger(options => options.Path = "/swagger/v1/swagger.json");

            app.UseSwaggerUi3(options =>

            {

                options.Path = "/swagger";

                options.DocumentPath = "/swagger/v1/swagger.json";

            });
            app.UseMvc();
        }
    }
}
