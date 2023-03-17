using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;
using System.Linq;

namespace MercedesBenzLogger
{
    public class CustomLogEventEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomLogEventEnricher() : this(new HttpContextAccessor())
        {
        }

        public CustomLogEventEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                logEvent.RemovePropertyIfPresent("ActionId");
                logEvent.RemovePropertyIfPresent("RequestId");
                logEvent.RemovePropertyIfPresent("ActionName");
                //logEvent.RemovePropertyIfPresent("RequestPath");
                logEvent.RemovePropertyIfPresent("SourceContext");

                logEvent.RemovePropertyIfPresent("State");
                logEvent.RemovePropertyIfPresent("MessageTemplate");

                if ((_httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated ?? false))
                {
                    var claims = (System.Security.Claims.ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
                    var user = claims.Claims.FirstOrDefault(x => x.Type.Contains("name"));
                    var role = claims.Claims.FirstOrDefault(x => x.Type.Contains("role"));
                    if (user != null)
                        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Usuario", user.Value));

                    if (role != null)
                        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Perfil", role.Value));

                    //var authenticationType = _httpContextAccessor.HttpContext.User.Identity.AuthenticationType;
                    //logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("TipoAutenticación", authenticationType));
                }

                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    string x_forwarded_for = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("IpClienteX", x_forwarded_for));
                }
                else
                {
                    //var prueba = _httpContextAccessor.HttpContext.Request;
                    string ipCliente = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    if (ipCliente == "::1")
                        ipCliente = "127.0.0.1";
                    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("IpCliente", ipCliente));

                    var remoteIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
                }

            }

        }

    }
}
