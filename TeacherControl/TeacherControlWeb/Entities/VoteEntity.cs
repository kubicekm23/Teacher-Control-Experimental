using System.ComponentModel.DataAnnotations;

namespace TeacherControlWeb.Entities;

public class VoteEntity
{
    public Guid Id { get; set; }

    [Required]
    public Guid TeacherId { get; set; }
    public TeacherEntity? Teacher { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public UserEntity? User { get; set; }

    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
