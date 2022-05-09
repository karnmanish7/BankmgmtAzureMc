using System.ComponentModel.DataAnnotations;

namespace CustomerService.API.Model.Customers
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}