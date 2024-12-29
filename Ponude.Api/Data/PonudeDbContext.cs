using Microsoft.EntityFrameworkCore;
using Ponude.Api.Models;

namespace Ponude.Api.Data
{
    public class PonudeDbContext : DbContext
    {
        public PonudeDbContext(DbContextOptions<PonudeDbContext> options) : base(options)
        {
        }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferItem> OfferItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
