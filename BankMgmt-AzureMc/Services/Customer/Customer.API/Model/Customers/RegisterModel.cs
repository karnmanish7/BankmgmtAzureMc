using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.API.Model.Customers
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        
        [Required(ErrorMessage = "Please Enter Password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter the Confirm Password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string Confirmpwd { get; set; }

        public string Address { get; set; }
        public string State { get; set; }
        public int Pincode { get; set; }
        public string Country { get; set; }
        public string PAN { get; set; }
        public string Email { get; set; }
        public int ContactNo { get; set; }
        public DateTime DOB { get; set; }
        
        public DateTime CreatedDtate { get; set; }
        public DateTime UpdatedDtate { get; set; }
    }
}