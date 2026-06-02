using System.ComponentModel.DataAnnotations;

namespace TeacherControlWeb.Entities;

public class ChatMessageEntity
{
    public Guid Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public UserEntity? User { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;

    public ChatMessageType Type { get; set; } = ChatMessageType.Text;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsApproved { get; set; } = true; // Default to true for text, maybe false for memes
}

public enum ChatMessageType
{
    Text,
    Meme
}
