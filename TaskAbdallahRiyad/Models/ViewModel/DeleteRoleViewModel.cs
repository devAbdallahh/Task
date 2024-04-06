using System.ComponentModel.DataAnnotations;

namespace TaskAbdallahRiyad.Models.ViewModel
{
    public class DeleteRoleViewModel
    {
        public string? RoleId { get; set; }
        [Required]
        public string? RoleName { get; set; }
    }
}
