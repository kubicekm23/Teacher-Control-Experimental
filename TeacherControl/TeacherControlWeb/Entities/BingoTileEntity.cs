using System.ComponentModel.DataAnnotations;

namespace TeacherControlWeb.Entities;

public class BingoTileEntity
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid BoardId { get; set; }
    public BingoBoardEntity? Board { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Text { get; set; } = string.Empty;
    
    [Range(0, 24)]
    public int Position { get; set; }
    
    public bool IsTriggered { get; set; } = false;
    
    public string? TriggeredByUserId { get; set; }
    public UserEntity? TriggeredByUser { get; set; }
}
