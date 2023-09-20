using Microsoft.EntityFrameworkCore;
using TotalSprReport.Models;

namespace TotalSprReport.Data
{
    public class SprDBContext : DbContext
    {
        public SprDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Proyek> Proyek { get; set;}
        public DbSet<Materials> Material { get; set;}
        
        public DbSet<Header_SPR> Header_SPR { get; set;}
        public DbSet<Detil_SPR> Detil_SPR { get;set;}
    }
}
