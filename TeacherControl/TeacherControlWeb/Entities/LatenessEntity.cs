using System.ComponentModel.DataAnnotations;

namespace TeacherControlWeb.Entities;

public class LatenessEntity
{
    public Guid Id { get; set; }

    [Required]
    public Guid TeacherId { get; set; }
    public TeacherEntity? Teacher { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public UserEntity? User { get; set; }

    [Range(1, 60)]
    public int Minutes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
