using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        //[Required(ErrorMessage = "Please Enter Password...")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[Required(ErrorMessage = "Please Enter the Confirm Password...")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm Password")]
        //[Compare("Password")]
        //public string Confirmpwd { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public int Pincode { get; set; }
        public string Country { get; set; }
        public string PAN { get; set; }
        public string Email { get; set; }
        public int ContactNo { get; set; }
        public DateTime DOB { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime CreatedDtate { get; set; }
        public DateTime UpdatedDtate { get; set; }
    }


    public enum AccountType
    {
        Savings,
        Current,
        Corporate,
        Goverment
    }
}

