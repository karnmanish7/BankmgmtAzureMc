using AutoMapper;
using CustomerService.API.Model.Customers;
using CustomerService.API.Entities;

namespace CustomerService.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Customer, UserModel>();
            CreateMap<RegisterModel, Customer>();
            //CreateMap<UpdateModel, Customer>();
        }
    }
}