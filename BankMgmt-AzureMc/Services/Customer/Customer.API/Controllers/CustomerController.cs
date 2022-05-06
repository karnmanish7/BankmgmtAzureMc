using Customer.API.DTOs;
using Customer.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            if (_customerRepository == null)
            {
                throw new NullReferenceException();
            }

            //this._customerRepository = customerRepository;
            _customerRepository = customerRepository;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> CreateRegister([FromBody] CustomerForRegisterDTO customerForRegisterDTO)
        {
            customerForRegisterDTO.Username = customerForRegisterDTO.Username.ToLower();
            if (await _customerRepository.UserExists(customerForRegisterDTO.Username))
                return BadRequest("Username already exists");

            var customerToCreate = new Customer
            {
                Username = customerForRegisterDTO.Username

            };
            var createdUser = await _customerRepository.Register(customerToCreate, customerForRegisterDTO.Password);
            return StatusCode(201);

           
        }
    }
}
