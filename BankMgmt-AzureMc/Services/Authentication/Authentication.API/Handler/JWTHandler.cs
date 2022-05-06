﻿using Authentication.API.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.API.Handler
{
    public class JWTHandler
    {
        private IConfiguration _config;

        public JWTHandler(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: new List<Claim>(), // claims (are used to filter the data)
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: signCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            // Validate the User Credentials using LDAP / Database
            if (login.Username == "admin")
            {
                //user = new UserModel { Username = "######", Password = "######" };
                user = new UserModel { Username = "admin", Password = "admin" };
            }
            return user;
        }
    }
}

