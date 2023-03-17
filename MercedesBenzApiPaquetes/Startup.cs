using MercedesBenzApiPaquetes.Contracts;
using MercedesBenzApiPaquetes.Services;
using MercedesBenzDBContext;
using MercedesBenzJwtAuthentication;
using MercedesBenzLibrary;
using MercedesBenzModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace MercedesBenzApiPaquetes
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
            string repositoryPath = ApplicationSettings.GetRepositoryPath();
            string urlApi = ApplicationSettings.GetApiServices();
            string urlApiExternal = urlApi;
            string urlcomparacionfacial = ApplicationSettings.GetUrlComparacionFacial();

            services.AddHttpContextAccessor();
            services.AddHeaderForwarded();
            services.AddCustomAuthenticationJwt();

            services.AddSingleton<ApplicationDBContext>();
            services.AddSingleton<Routes>(new Routes() { RepositoryPath = repositoryPath, UrlApiServicesExternal = urlApiExternal });
            services.AddHttpClient<IValidacionService, ValidacionService>(http => http.BaseAddress = new Uri(urlApiExternal));
            services.AddHttpClient<IComparacionFacialService, ComparacionFacialService>(http => http.BaseAddress = new Uri(urlcomparacionfacial));

            services.AddScoped<AuthenticationControllerFilter>();
            services.AddScoped<IPaqueteRepository, PaqueteRepository>();
            services.AddScoped<IDataService, DataService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MercedesBenzApiPaquetes", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MercedesBenzApiPaquetes v1"));
            }

            app.UseRouting();

            app.UseForwardedHeaders();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
