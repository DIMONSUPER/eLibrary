using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BGNet.TestAssignment.DataAccess.Entities.Configurations;

public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
{
    #region -- IEntityTypeConfiguration implementation --

    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.PublicationYear).IsRequired();
        builder.Property(x => x.Genre).IsRequired();
        builder.Property(x => x.AuthorId).IsRequired();

        builder
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);
    }

    #endregion
}
