using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<FavoritesPurchases> favoritesPurchases { get; set; }
        public DbSet<Discounts> discounts { get; set; }
        public DbSet<QuantityAndSales> quantityAndSales { get; set; }
      //  public virtual DbSet<BookBuy> bookBuys { get; set; }

        public ApplicationContext()
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source = BookShop.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  modelBuilder.Entity<BookBuy>().HasNoKey();
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
           // configurationBuilder.DefaultTypeMapping<BookBuy>();
        }
    }
}
