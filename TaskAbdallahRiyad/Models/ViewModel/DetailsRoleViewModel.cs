using System.ComponentModel.DataAnnotations;

namespace TaskAbdallahRiyad.Models.ViewModel
{
    public class DetailsRoleViewModel
    {
        public string? RoleId { get; set; }
        [Required]
        public string? RoleName { get; set; }
    }
}
