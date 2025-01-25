using System.ComponentModel.DataAnnotations;

namespace sdq.DTOs
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "Role Name is required.")]
        public string RoleName { get; set; } = null!;
    }
}
