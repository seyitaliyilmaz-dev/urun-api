using Microsoft.EntityFrameworkCore;

namespace urun_api;

public class SirketDbContext : DbContext
{
    public SirketDbContext(DbContextOptions<SirketDbContext> options) : base(options) { }

    public DbSet<Urun> Urunler { get; set; }
}