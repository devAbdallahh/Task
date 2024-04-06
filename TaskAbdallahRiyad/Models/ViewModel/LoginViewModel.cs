using System.ComponentModel.DataAnnotations;

namespace TaskAbdallahRiyad.Models.ViewModel
{
    public class LoginViewModel
    {
        //Email
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        //Password
        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        //RememberMe
        [Required(ErrorMessage = "Do you want to remember the registration?")]
        public bool RememberMe { get; set; }
    }
}
