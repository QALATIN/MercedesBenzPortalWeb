using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace MercedesBenzLibrary
{
    public static class CustomServices
    {
        public static void AddHeaderForwarded(this IServiceCollection services)
        {
            string knownProxies = ApplicationSettings.GetKnownProxies();

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

        }
    }
}
