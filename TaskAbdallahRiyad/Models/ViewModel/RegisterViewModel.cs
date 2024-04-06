using System.ComponentModel.DataAnnotations;

namespace TaskAbdallahRiyad.Models.ViewModel
{
    public class RegisterViewModel
    {
        //FullName
        [Required(ErrorMessage = "Please Enter Full Name  ")]
        public string? FullName { get; set; }

        //Email
        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress]
        public string? Email { get; set; }

        //ConfirmEmail
        [Required(ErrorMessage = "Please Enter Confirm Email Address")]
        [EmailAddress]
        [Compare("Email", ErrorMessage = "Please Email and ConfirmEmail not match")]
        public string? ConfirmEmail { get; set; }

        //Password
        [Required(ErrorMessage = " Please Enter Password ")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        //ConfirmPassword
        [Required(ErrorMessage = "Please Enter Password ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword not match")]
        public string? ConfirmPassword { get; set; }

        //City
        [Required(ErrorMessage = " Please Enter City ")]
        public string? City { get; set; }

        //Phone
        [Required(ErrorMessage = "Please Enter Phone Number ")]
        public string? PhoneNumber { get; set; }
    }
}
