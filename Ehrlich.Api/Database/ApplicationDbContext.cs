using Ehrlich.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ehrlich.Api.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<PizzaType>()
                .HasMany(p => p.Ingredients)
                .WithMany();

            modelBuilder.Entity<Pizza>()
                .Property(p => p.Size)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PizzaType> PizzaTypes { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaCategory> PizzaCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedItem> OrderedItems { get; set; }

    }
}
