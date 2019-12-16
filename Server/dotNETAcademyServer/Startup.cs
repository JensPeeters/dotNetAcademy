using Business_layer;
using Business_layer.Interfaces;
using Data_layer;
using Data_layer.Interfaces;
using Data_layer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Data_layer.Filter;
using Business_layer.Mapping;
using Business_layer.Interfaces.Mapping;
using ExtensionMethods;

namespace dotNETAcademyServer
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
            services.AddDbContext<DatabaseContext>(
                //options => options.UseSqlServer(
                //    Configuration.GetConnectionString("DefaultConnection")
                //)
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Data layer")
                )
            );
            //Dependency injection configuration
            services.AddConfig();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,DatabaseContext context)
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
            //DbInitialiser.Initialize(context);
            context.Database.Migrate();
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                         .AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
