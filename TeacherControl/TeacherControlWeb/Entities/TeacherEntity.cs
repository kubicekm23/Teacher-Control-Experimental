using System.ComponentModel.DataAnnotations;

namespace TeacherControlWeb.Entities;

public class TeacherEntity
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Subject { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public ICollection<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();
    public ICollection<LatenessEntity> Latenesses { get; set; } = new List<LatenessEntity>();
    public ICollection<VoteEntity> Votes { get; set; } = new List<VoteEntity>();
}
