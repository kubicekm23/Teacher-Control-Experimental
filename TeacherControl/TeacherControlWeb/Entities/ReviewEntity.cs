using System.ComponentModel.DataAnnotations;

namespace TeacherControlWeb.Entities;

public class ReviewEntity
{
    public Guid Id { get; set; }

    [Required]
    public Guid TeacherId { get; set; }
    public TeacherEntity? Teacher { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public UserEntity? User { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(2000)]
    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsAnonymous { get; set; }
}
