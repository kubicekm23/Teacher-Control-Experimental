using Microsoft.AspNetCore.Identity;

namespace TeacherControlWeb.Entities;

public class UserEntity : IdentityUser
{
    public bool IsBanned { get; set; }
}
