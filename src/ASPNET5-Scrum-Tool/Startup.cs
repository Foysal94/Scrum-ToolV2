using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using ASPNET5_Scrum_Tool.Models;
using Microsoft.Data.Entity;

namespace ASPNET5_Scrum_Tool
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCaching();
            services.AddSession();

            // Add framework services.
            services.AddMvc();
           
            var connection = @"Server=(localdb)\mssqllocaldb;Database=ScrumToolDB;Trusted_Connection=True;MultipleActiveResultSets=false";
            var azureConnection =
                @"Server=tcp:foysal94.database.windows.net,1433;Database=ScrumToolDB;
                    User ID=Foysal94@foysal94;Password=Flatron94;
                    Encrypt=True;TrustServerCertificate=False;
                    Connection Timeout=30;MultipleActiveResultSets=false;";
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ScrumToolDB>(options =>
                {
                    options.UseSqlServer(azureConnection);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSession();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            */
             
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
