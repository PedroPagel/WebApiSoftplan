using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.CalculoJuros;
using Services.ShowMeTheCode;
using Services.TaxaJuros;
using System;
using System.IO;
using System.Net.Http;

namespace WebApiSoftplan2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static string GetXmlSwaggerPath()
        {
            return Path.Combine(AppContext.BaseDirectory, "WebApiSoftplan.xml");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Documentação",
                    Description = "Documentação WebApiSoftplan",
                    Contact = new OpenApiContact
                    {
                        Name = "Pedro Pagel",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/pedro-pagel-92185aa7/"),
                    }
                });
                c.IncludeXmlComments(GetXmlSwaggerPath());
            });

            services.AddSingleton<ICalculoJurosService, CalculoJurosService>();
            services.AddSingleton<ITaxaJurosService, TaxaJurosService>();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IShowMeTheCode, ShowMeTheCode>();
            services.AddTransient<ApiGetter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiSoftplan2 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
