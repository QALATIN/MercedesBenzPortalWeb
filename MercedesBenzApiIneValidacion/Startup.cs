using MercedesBenzApiIneValidacion.Contracts;
using MercedesBenzDBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MercedesBenzApiIneValidacion
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
            //El encabezado X - Forwarded - For(XFF) es un encabezado estándar para identificar la dirección IP de origen de un cliente que se conecta a un servidor web a través de un proxy HTTP o un balanceador de carga. 
            //El encabezado es una lista de direcciones IP conocidas separadas por comas
            var knownProxies = Configuration["KnownProxies"];

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = null;
                options.KnownProxies.Clear();
                foreach (var ip in knownProxies.Split(new char[] { ',' }))
                {
                    options.KnownProxies.Add(IPAddress.Parse(ip));
                }
            });
            services.AddHttpContextAccessor();  // Utilizado para Personalizar Serilog

            services.AddSingleton<ApplicationDBContext>();

            services.AddScoped<IIneRepository, IneRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MercedesBenzApiIneValidacion", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MercedesBenzApiIneValidacion v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
