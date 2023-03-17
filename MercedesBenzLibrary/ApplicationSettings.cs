using Microsoft.Extensions.Configuration;
using System.IO;

namespace MercedesBenzLibrary
{
    public static class ApplicationSettings
    {
        public static string GetConnectionString() => GetConfiguration("connectionString");

        public static string GetEnvironment() => GetConfiguration("environment");

        public static string GetKnownProxies() => GetConfiguration("knownProxies");

        public static string GetJwtKey() => GetConfiguration("jwtKey");

        public static string GetJwtIssuer() => GetConfiguration("jwtIssuer");

        public static string GetJwtAudience() => GetConfiguration("jwtAudience");

        public static string GetRepositoryPath() => GetConfiguration("repositoryPath");

        public static string GetApiAuthentication() => GetConfiguration("ApiAuthentication");

        public static string GetApiServices() => GetConfiguration("ApiServices");

        public static string GetApiServicesExternal() => GetConfiguration("ApiServicesExternal");

        public static string GetApkName() => GetConfiguration("apkName");

        public static string GetApkPath() => GetConfiguration("apkPath");

        public static string GetPortalVersion() => GetConfiguration("portalVersion");

        public static string GetUrlComparacionFacial() => GetConfiguration("comparacionFacial");

        private static string GetConfiguration(string parametro)
        {
            string data = string.Empty;
            string ambiente;

            string directory = Validaciones.GetPathConfigurationFile();
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(directory))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            ambiente = configuration["Parametros:Ambiente"];
            if (parametro == "connectionString")
            {
                data = configuration[$"ConnectionStrings:ConnectionString{ambiente}"];
            }
            else if (parametro == "repositoryPath")
            {
                data = configuration[$"Routes:RepositoryPath{ambiente}"];
            }
            else if (parametro == "ApiAuthentication")
            {
                data = configuration[$"Routes:UrlApiAuthentication{ambiente}"];
            }
            else if (parametro == "ApiServices")
            {
                data = configuration[$"Routes:UrlApiServices{ambiente}"];
            }
            else if (parametro == "ApiServicesExternal")
            {
                data = configuration[$"Routes:UrlApiServicesExternal{ambiente}"];
            }
            else if (parametro == "environment")
            {
                data = configuration["Parametros:Ambiente"];
            }
            else if (parametro == "knownProxies")
            {
                data = configuration["KnownProxies"];
            }
            else if (parametro == "jwtKey")
            {
                data = configuration["Jwt:Key"];
            }
            else if (parametro == "jwtIssuer")
            {
                data = configuration["Jwt:Issuer"];
            }
            else if (parametro == "jwtAudience")
            {
                data = configuration["Jwt:Audience"];
            }
            else if (parametro == "apkName")
            {
                data = configuration["Parametros:ApkName"];
            }
            else if (parametro == "apkPath")
            {
                data = configuration["Parametros:ApkPath"];
            }
            else if (parametro == "portalVersion")
            {
                data = configuration["Parametros:PortalVersion"];
            }
            else if (parametro == "comparacionFacial")
            {
                data = configuration[$"Routes:UrlComparacionFacial{ambiente}"];
            }
            return data;
        }
    }
}
