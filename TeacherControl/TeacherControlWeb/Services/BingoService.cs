using Microsoft.EntityFrameworkCore;
using TeacherControlWeb.Data;
using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Services;

public interface IBingoService
{
    Task<BingoBoardEntity> GetCurrentBoardAsync();
    Task<bool> TriggerTileAsync(Guid tileId, string userId);
}

public class BingoService : IBingoService
{
    private readonly AppDbContext _context;
    private static readonly string[] TilePool = {
        "Učitel zapomněl klíče",
        "Učitel přišel včas",
        "Učitel vypráví historku",
        "Učitel se zasmál vlastnímu vtipu",
        "Učitel pohrozil testem",
        "Někdo vyrušuje",
        "Učitel si plete jména",
        "Učitel zapomněl smazat tabuli",
        "Učitel pije kávu",
        "Učitel si povzdechl",
        "Učitel opravuje písemky",
        "Učitel mluví o své rodině",
        "Učitel si upravuje brýle",
        "Učitel hledá fixu",
        "Učitel ztratil nit",
        "Učitel cituje klasika",
        "Učitel chválí třídu",
        "Učitel se diví, že někdo chybí",
        "Učitel mluví o maturitě",
        "Učitel zkouší u tabule",
        "Učitel pustil film",
        "Učitel vypráví o dovolené",
        "Učitel mluví o politice",
        "Učitel se zasekl u počítače",
        "Učitel zapomněl na úkol"
    };

    public BingoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BingoBoardEntity> GetCurrentBoardAsync()
    {
        var today = DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Utc);
        var board = await _context.BingoBoards
            .Include(b => b.Tiles)
            .Include(b => b.Winner)
            .FirstOrDefaultAsync(b => b.Date == today);

        if (board == null)
        {
            board = await CreateDailyBoard(today);
        }

        return board;
    }

    private async Task<BingoBoardEntity> CreateDailyBoard(DateTime date)
    {
        var board = new BingoBoardEntity { Date = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc) };
        _context.BingoBoards.Add(board);
        await _context.SaveChangesAsync();

        var random = new Random((int)date.Ticks);
        var selectedTiles = TilePool.OrderBy(x => random.Next()).Take(25).ToList();

        for (int i = 0; i < 25; i++)
        {
            var tile = new BingoTileEntity
            {
                BoardId = board.Id,
                Text = selectedTiles[i],
                Position = i
            };
            _context.BingoTiles.Add(tile);
        }

        await _context.SaveChangesAsync();
        
        // Reload with tiles
        return await _context.BingoBoards
            .Include(b => b.Tiles)
            .FirstAsync(b => b.Id == board.Id);
    }

    public async Task<bool> TriggerTileAsync(Guid tileId, string userId)
    {
        var tile = await _context.BingoTiles
            .Include(t => t.Board)
            .ThenInclude(b => b!.Tiles)
            .FirstOrDefaultAsync(t => t.Id == tileId);

        if (tile == null || tile.IsTriggered || tile.Board!.IsWon)
            return false;

        tile.IsTriggered = true;
        tile.TriggeredByUserId = userId;

        if (CheckWin(tile.Board))
        {
            tile.Board.IsWon = true;
            tile.Board.WinnerId = userId;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    private bool CheckWin(BingoBoardEntity board)
    {
        var tiles = board.Tiles.OrderBy(t => t.Position).ToList();
        var grid = new bool[5, 5];
        foreach (var tile in tiles)
        {
            grid[tile.Position / 5, tile.Position % 5] = tile.IsTriggered;
        }

        // Rows and Columns
        for (int i = 0; i < 5; i++)
        {
            if (Enumerable.Range(0, 5).All(j => grid[i, j])) return true;
            if (Enumerable.Range(0, 5).All(j => grid[j, i])) return true;
        }

        // Diagonals
        if (Enumerable.Range(0, 5).All(i => grid[i, i])) return true;
        if (Enumerable.Range(0, 5).All(i => grid[i, 4 - i])) return true;

        return false;
    }
}
