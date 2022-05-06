using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.Repository
{
   public interface ICustomerRepository
    {
        Task<Customer> Register(Customer customer, string password);
        Task<bool> UserExists(string username);
    }
}
