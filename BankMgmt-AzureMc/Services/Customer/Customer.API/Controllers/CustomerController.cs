using AutoMapper;
using Customer.API.DTOs;
using Customer.API.Model.Customers;
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
        private IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository,IMapper mapper)
        {
            //if (_customerRepository == null)
            //{
            //    throw new NullReferenceException();
            //}

            //this._customerRepository = customerRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
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

        [Route("register1")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<Customer>(model);

            try
            {
                // create user
                await _customerRepository.Create(user, model.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
