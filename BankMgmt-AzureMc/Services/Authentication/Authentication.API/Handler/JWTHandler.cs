using AuthenticationService.API.Model;
using CustomerService.API.DBContext;
using CustomerService.API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace AuthenticationService.API.Handler
{
    public class JWTHandler
    {
        private IConfiguration _config;
        private BankMgmtDBContext _bankMgmtDBContext;

        public JWTHandler(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJSONWebToken(Customer userInfo)
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

        public Customer AuthenticateUser(UserModel login)
        {
            //UserModel user = null;

            //// Validate the User Credentials using LDAP / Database
            //if (login.Username == "admin")
            //{
            //    //user = new UserModel { Username = "######", Password = "######" };
            //    user = new UserModel { Username = "admin", Password = "admin" };
            //}
            //return user;


            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
                return null;

            var user = _bankMgmtDBContext.Customers.SingleOrDefault(x => x.Username == login.Username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }


        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}

