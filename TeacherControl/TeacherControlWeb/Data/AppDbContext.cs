using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Data;

public class AppDbContext : IdentityDbContext<UserEntity>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<TeacherEntity> Teachers { get; set; } = null!;
    public DbSet<ReviewEntity> Reviews { get; set; } = null!;
    public DbSet<LatenessEntity> Latenesses { get; set; } = null!;
    public DbSet<VoteEntity> Votes { get; set; } = null!;
    public DbSet<ChatMessageEntity> ChatMessages { get; set; } = null!;
    public DbSet<MemeEntity> Memes { get; set; } = null!;
    public DbSet<BingoBoardEntity> BingoBoards { get; set; } = null!;
    public DbSet<BingoTileEntity> BingoTiles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BingoTileEntity>()
            .HasOne(t => t.Board)
            .WithMany(b => b.Tiles)
            .HasForeignKey(t => t.BoardId);

        builder.Entity<ReviewEntity>()
            .HasOne(r => r.Teacher)
            .WithMany(t => t.Reviews)
            .HasForeignKey(r => r.TeacherId);

        builder.Entity<LatenessEntity>()
            .HasOne(l => l.Teacher)
            .WithMany(t => t.Latenesses)
            .HasForeignKey(l => l.TeacherId);

        builder.Entity<VoteEntity>()
            .HasOne(v => v.Teacher)
            .WithMany(t => t.Votes)
            .HasForeignKey(v => v.TeacherId);
    }
}
