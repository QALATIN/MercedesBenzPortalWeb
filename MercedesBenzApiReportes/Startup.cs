using MercedesBenzApiReportes.Contracts;
using MercedesBenzApiReportes.Helpers;
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

namespace MercedesBenzApiReportes
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
            services.AddHttpContextAccessor();  
            services.AddHeaderForwarded();
            services.AddCustomAuthenticationJwt();

            services.AddSingleton<ApplicationDBContext>();
            services.AddSingleton<Routes>(new Routes() { RepositoryPath = repositoryPath });

            services.AddScoped<IReporteRepository, ReporteRepository>();
            services.AddScoped<AuthenticationControllerFilter>();
            services.AddScoped<ExcelHelper>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MercedesBenzApiReportes", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MercedesBenzApiReportes v1"));
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
