
using Microsoft.EntityFrameworkCore;
using WPFBrowser.Models;

namespace WPFBrowser.Data;

public class AppDbContext : DbContext
{

    public DbSet<HistoryElement> History { get; set; }
    public DbSet<Tab> Tabs { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=browser.db3");
    }

}
