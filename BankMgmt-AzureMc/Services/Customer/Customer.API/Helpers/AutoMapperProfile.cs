using AutoMapper;
using Customer.API.Model.Customers;

namespace Customer.API.Helpers
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