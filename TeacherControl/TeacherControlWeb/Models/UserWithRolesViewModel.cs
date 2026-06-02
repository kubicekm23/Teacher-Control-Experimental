using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Models;

public class UserWithRolesViewModel
{
    public UserEntity UserEntity { get; set; } = null!;
    public IList<string> Roles { get; set; } = [];
    public IList<string> AllRoles { get; set; } = [];
}
