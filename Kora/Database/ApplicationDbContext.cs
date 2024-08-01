using Kora.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kora.Database;

public class ApplicationDbContext : DbContext
{
    public string DbPath { get; }
    public DbSet<User> Users { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions options)  : base(options)
    {
        var folder = Environment.SpecialFolder.MyDocuments;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "Database/kora.db");
    }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    // protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source=/Users/wisdomoppongyeboah/Documents/Database/kora.db");
}