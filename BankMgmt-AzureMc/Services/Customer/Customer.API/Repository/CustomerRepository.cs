using CustomerService.API.DBContext;
using CustomerService.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.API.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private BankMgmtDBContext _bankMgmtDBContext;

        public CustomerRepository(BankMgmtDBContext bankMgmtDBContext)
        {
            _bankMgmtDBContext = bankMgmtDBContext;
        }

        public async Task<Customer> Register(Customer Customer, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            Customer.PasswordHash = passwordHash;
            Customer.PasswordSalt = passwordSalt;
            await _bankMgmtDBContext.Customers.AddAsync(Customer);
            await _bankMgmtDBContext.SaveChangesAsync();
            return Customer;
        }

        public async Task<Customer> Create(Customer Customer, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_bankMgmtDBContext.Customers.Any(x => x.Username == Customer.Username))
                //throw new AppException("Username \"" + user.Username + "\" is already taken");
                throw new Exception("Username \"" + Customer.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            Customer.PasswordHash = passwordHash;
            Customer.PasswordSalt = passwordSalt;

            _bankMgmtDBContext.Customers.Add(Customer);
            _bankMgmtDBContext.SaveChanges();

            return Customer;
        }
      
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt = hmac.Key;
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }

            }
            return true;
        }

     
        public async Task<bool> UserExists(string username)
        {
            if (await _bankMgmtDBContext.Customers.AnyAsync(x => x.Username == username))
                return true;
            return false;
        }

    }
}
