using Infrastructure.Context;
using Infrastructure.ObjectsDao;
using Infrastructure.ObjectsDao.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.AreaPredial;
using Services.AreaPredial.Interfaces;
using Services.Pessoas;
using System;
using System.IO;

namespace WebApiPorterGroup
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
            return Path.Combine(AppContext.BaseDirectory, "WebApiPorterGroup.xml");
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
                    Description = "Documentação WebApiPorterGroup",
                    Contact = new OpenApiContact
                    {
                        Name = "Pedro Pagel",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/pedro-pagel-92185aa7/"),
                    }
                });
                c.IncludeXmlComments(GetXmlSwaggerPath());
            });

            services.AddEntityFrameworkInMemoryDatabase()
            .AddDbContext<WebApiContext>((sp, options) =>
            {
                options.UseInMemoryDatabase(databaseName: "WebApi").UseInternalServiceProvider(sp);
            });

            services.AddTransient<WebApiContext>();
            services.AddTransient<ICondominioService, CondominioService>();
            services.AddTransient<IBlocoService, BlocoService>();
            services.AddTransient<IMoradorService, MoradorService>();
            services.AddTransient<IApartamentoService, ApartamentoService>();
            services.AddTransient<IMoradorDAO, MoradorDAO>();
            services.AddTransient<IBlocoDAO, BlocoDAO>();
            services.AddTransient<ICondominioDAO, CondominioDAO>();
            services.AddTransient<IApartamentoDAO, ApartamentoDAO>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebApiContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiPorterGroup v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            context.AdicionarDados();
        }
    }
}
