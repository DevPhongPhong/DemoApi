using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HyperAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        private IConfiguration _configuration;
        public HyperAuthorizeFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorize = context.HttpContext.Request.Headers["Authorization"];
            var token = authorize.FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    messages = "Dữ liệu truyền thiếu Token.",
                    code = 401,
                    success = false
                });
                return;
            }
            var principal = AuthenticateJwtToken(token);
            if (principal.Result == null)
            {
                // not logged in
                context.Result = new UnauthorizedObjectResult(new
                {
                    messages = "Bạn không có quyền truy cập.",
                    code = 401,
                    success = false
                });
                return;
            }
            context.HttpContext.Items["User"] = principal;
        }
        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            if (ValidateToken(token, out var publickey))
            {
                IPrincipal user = new ClaimsPrincipal();

                return Task.FromResult(user);
            }
            return Task.FromResult<IPrincipal>(null);
        }
        private bool ValidateToken(string token, out string publicKey)
        {
            publicKey = null;

            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            return true;
        }
        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;
                var key = _configuration["AppSettings:SecretKey"];
                var symmetricKey = Encoding.UTF8.GetBytes(key);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
