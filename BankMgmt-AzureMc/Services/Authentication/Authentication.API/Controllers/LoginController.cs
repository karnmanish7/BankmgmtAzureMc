using AuthenticationService.API.Handler;
using AuthenticationService.API.Model;
using CustomerService.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;


        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {

            IActionResult response = Unauthorized();
            JWTHandler jWTHandler = new JWTHandler(_config);

            var user = jWTHandler.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = jWTHandler.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

    }
}
