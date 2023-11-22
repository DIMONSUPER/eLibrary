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

    public required DbSet<User> Users { get; set; }
    public required DbSet<Author> Authors { get; set; }
    public required DbSet<Book> Books { get; set; }

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
