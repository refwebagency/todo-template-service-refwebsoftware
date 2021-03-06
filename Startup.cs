using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TodoTemplateService.Data;
using TodoTemplateService.Controllers;
using TodoTemplateService.AsyncDataServices;
using TodoTemplateService.EventProcessing;

namespace TodoTemplateService
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
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("todoTemplate"));

            services.AddScoped<ITodoTemplateRepo, TodoTemplateRepo>();
            services.AddHttpClient<TodoTemplateController>();
            services.AddHostedService<MessageBusSuscriber>();
            services.AddTransient<IEventProcessor, EventProcessor>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCors(options =>     //permet d'autoriser à un client distant de requeter sur le service
            {
                options.AddPolicy("AllowAll", p =>  
                {
                    p.WithOrigins("http://localhost:4200") //port d'angular
                    .AllowAnyHeader() //autorise toutes types de données
                    .AllowAnyMethod(); //autorise toutes les requetes, GET POST PUT DELETE
                    });
                });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoTemplateService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>{ 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoTemplateService v1");
                    c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
