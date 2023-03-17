using MercedesBenzApiAgencias.Contracts;
using MercedesBenzDBContext;
using MercedesBenzJwtAuthentication;
using MercedesBenzLibrary;
using MercedesBenzLogger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MercedesBenzApiAgencias
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
            services.AddHttpContextAccessor();  
            services.AddHeaderForwarded();
            services.AddCustomAuthenticationJwt();

            services.AddSingleton<ApplicationDBContext>();

            services.AddScoped<AuthenticationControllerFilter>();
            services.AddScoped<IAgenciaRepository, AgenciaRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MercedesBenzApiAgencias", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MercedesBenzApiAgencias v1"));
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
