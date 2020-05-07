using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Raspberry.Services;
using System;

namespace Raspberry.Configuration
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {

        private readonly IOptions<Configs> _config;

        public AuthController(IOptions<Configs> config)
        {
            _config = config;
        }

        [HttpPost]
        public object Post([FromBody]string username, string password)
        {
            try
            {
                if (!new SimpleAuthService(_config.Value.Auth).ValidateUser(username, password))
                    return new { success = false, error = "Inválid username or password" };

                var jwtTool = new JwtTool(_config.Value.JwtSettings);
                return new
                {
                    success = true,
                    token = jwtTool.GenerateToken(username)
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    error = JsonConvert.SerializeObject(ex)
                };
            }
        }
        
    }
}
