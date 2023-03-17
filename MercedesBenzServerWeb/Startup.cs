using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MercedesBenzServerWeb.Services;
using System;
using Microsoft.AspNetCore.Components.Authorization;
using MercedesBenzServerWeb.Authentication;
using MercedesBenzModel;
using MercedesBenzServerWeb.Helpers;
using MercedesBenzLibrary;

namespace MercedesBenzServerWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHeaderForwarded();

            string ambiente = ApplicationSettings.GetEnvironment();
            string urlApiAutenticacion = ApplicationSettings.GetApiAuthentication();
            string urlApi = ApplicationSettings.GetApiServices();
            string urlApiAgencias = urlApi;
            string urlApiBusquedas = urlApi;
            string urlApiCatalogos = urlApi;
            string urlApiUsuarios = urlApi;
            string urlApiPaquetes = urlApi;
            string urlApiRecuperacion = urlApi;
            string urlApiReportes = urlApi;
            string urlApiGeneral = urlApi;
            string urlApiValidacionesExternal = urlApi;

            //urlApiCatalogos = "http://localhost:44352/api/";
            //urlApiAgencias = "http://localhost:44353/api/";
            //urlApiUsuarios = "http://localhost:44354/api/";
            //urlApiBusquedas = "http://localhost:44355/api/";
            //urlApiReportes = "http://localhost:44356/api/";
            //urlApiPaquetes = "http://localhost:44357/api/";
            //urlApiRecuperacion = "http://localhost:44358/api/";
            //urlApiGeneral = "http://localhost:44359/api/";
            //urlApiValidacionesExternal = "https://localhost:44389/api/";

            services.AddAuthenticationCore();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();

            services.AddHttpClient<IAutenticacionService, AutenticacionService>(http => http.BaseAddress = new Uri(urlApiAutenticacion));
            services.AddHttpClient<IAgenciaService, AgenciaService>(http => http.BaseAddress = new Uri(urlApiAgencias));
            services.AddHttpClient<IBusquedaService, BusquedaService>(http => http.BaseAddress = new Uri(urlApiBusquedas));
            services.AddHttpClient<ICatalogoService, CatalogoService>(http => http.BaseAddress = new Uri(urlApiCatalogos));
            services.AddHttpClient<IPaqueteService, PaqueteService>(http => http.BaseAddress = new Uri(urlApiPaquetes));
            services.AddHttpClient<IRecuperacionService, RecuperacionService>(http => http.BaseAddress = new Uri(urlApiRecuperacion));
            services.AddHttpClient<IReporteService, ReporteService>(http => http.BaseAddress = new Uri(urlApiReportes));
            services.AddHttpClient<IUsuarioService, UsuarioService>(http => http.BaseAddress = new Uri(urlApiUsuarios));
            services.AddHttpClient<IGeneralService, GeneralService>(http => http.BaseAddress = new Uri(urlApiGeneral));

            services.AddHttpClient<IValidacionesExternasService, ValidacionesExternasService>(http => http.BaseAddress = new Uri(urlApiValidacionesExternal));
            
            services.AddScoped<RepositoryService>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthentication>();
            services.AddScoped<LocalStorage>();
            services.AddScoped<AppState>();
            services.AddScoped<UserCredential>();
            services.AddScoped<BusquedaRequest>();
            services.AddScoped<Parametros>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
