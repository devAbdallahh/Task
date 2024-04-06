using System.ComponentModel.DataAnnotations;

namespace TaskAbdallahRiyad.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Email ")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Enter Phone Number")]
        public int PhoneNumber { get; set; }
    }
}
