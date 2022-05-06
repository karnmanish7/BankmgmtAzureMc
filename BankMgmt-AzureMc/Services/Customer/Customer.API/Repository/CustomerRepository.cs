using Customer.API.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private BankMgmtDBContext _bankMgmtDBContext;

        public CustomerRepository(BankMgmtDBContext bankMgmtDBContext)
        {
            _bankMgmtDBContext = bankMgmtDBContext;
        }

        public async Task<Customer> Register(Customer customer, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
            await _bankMgmtDBContext.Registrations.AddAsync(customer);
            await _bankMgmtDBContext.SaveChangesAsync();
            return customer;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
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
            if (await _bankMgmtDBContext.Registrations.AnyAsync(x => x.Username == username))
                return true;
            return false;
        }

    }
}
