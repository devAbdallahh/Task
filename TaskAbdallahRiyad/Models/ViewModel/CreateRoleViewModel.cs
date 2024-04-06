using System.ComponentModel.DataAnnotations;

namespace TaskAbdallahRiyad.Models.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
