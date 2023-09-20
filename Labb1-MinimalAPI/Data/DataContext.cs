using Labb1_MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1_MinimalAPI.Data {
    public class DataContext : DbContext {

        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book() {
                    Id = Guid.NewGuid(),
                    Title = "Spooky Book",
                    Author = "Tobias",
                    Genre = "Spooky",
                    Description = "Very Spooky",
                    Year = DateTime.Now,
                    IsLoanAble = true,
                },
                new Book() {
                    Id = Guid.NewGuid(),
                    Title = "Spooky Book 2",
                    Author = "Anas",
                    Genre = "Spooky",
                    Description = "More Spookier",
                    Year = DateTime.Now,
                    IsLoanAble = true,
                },
                new Book() {
                    Id = Guid.NewGuid(),
                    Title = "Bames Jond",
                    Author = "Lucas",
                    Genre = "Spy",
                    Description = "Very Spy",
                    Year = DateTime.Now,
                    IsLoanAble = false,
                }
            );
        }

    }
}
