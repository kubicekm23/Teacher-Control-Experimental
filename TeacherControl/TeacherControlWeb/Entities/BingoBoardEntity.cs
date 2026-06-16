using System.ComponentModel.DataAnnotations;

namespace TeacherControlWeb.Entities;

public class BingoBoardEntity
{
    public Guid Id { get; set; }
    
    [Required]
    public DateTime Date { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Utc);
    
    public bool IsWon { get; set; } = false;
    
    public string? WinnerId { get; set; }
    public UserEntity? Winner { get; set; }
    
    public ICollection<BingoTileEntity> Tiles { get; set; } = new List<BingoTileEntity>();
}
