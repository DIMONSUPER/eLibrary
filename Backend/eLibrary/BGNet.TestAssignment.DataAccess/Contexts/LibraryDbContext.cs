using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.DataAccess.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.DataAccess.Contexts;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions options) : base(options)
    {
    }

    #region -- Public properties --

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;

    #endregion

    #region -- Overrides --

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
    }

    #endregion
}
