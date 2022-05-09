using CustomerService.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.API.Repository
{
   public interface ICustomerRepository
    {
        Task<Customer> Register(Customer Customer, string password);
        Task<bool> UserExists(string username);
        Task<Customer> Create(Customer user, string password);
    }
}
