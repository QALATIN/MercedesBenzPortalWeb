using MercedesBenzDBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace MercedesBenzJwtAuthentication
{
    public class AuthenticationControllerFilter : IActionFilter
    {
        private readonly ApplicationDBContext _context;

        public AuthenticationControllerFilter(ApplicationDBContext context)
        {
            _context = context;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new UnauthorizedObjectResult("");
            }
            else
            {
                string autorizacion = context.HttpContext.Request.Headers["Authorization"].ToString();
                var claims = (System.Security.Claims.ClaimsIdentity)context.HttpContext.User.Identity;
                var user = claims.Claims.FirstOrDefault(x => x.Type.Contains("name"));
                if (user == null)
                    context.Result = new UnauthorizedObjectResult("");
                else
                {
                    bool tokenValido = _context.TokenValido(user.Value, autorizacion);
                    if (!tokenValido)
                        context.Result = new UnauthorizedObjectResult("");
                }
            }
        }
    }
}
