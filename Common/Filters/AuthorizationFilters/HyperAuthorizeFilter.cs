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

namespace Common.Filters.AuthorizeFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HyperAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        public HyperAuthorizeFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorize = context.HttpContext.Request.Headers["Authorization"];
            var token = authorize.FirstOrDefault()?.Split(" ").Last();
            var path = context.HttpContext.Request.Path.Value;
            var role = path.Split("/")[2];
            string message = "";

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
            string id = "-1";
            if (!ValidateToken(token, role, out message, out id))
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    messages = "Invalid Token",
                    code = 401,
                    success = false
                });
                return;
            }
            context.HttpContext.Request.Headers["id"] = id;
        }
        private bool ValidateToken(string token, string role, out string message, out string id)
        {
            message = "Invalid Token";
            id = "-1";
            try
            {
                var key = _configuration["AppSettings:SecretKey"];
                var symmetricKey = Encoding.UTF8.GetBytes(key);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                var claimRole = jwtToken.Claims.First(c => c.Type == "role");

                if (jwtToken == null)
                    return false;
                if (principal == null)
                    return false;
                if (claimRole.Value.ToLower() != role.ToLower())
                    return false;

                message = "Success";
                id = jwtToken.Claims.First(c => c.Type == "id").Value;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
