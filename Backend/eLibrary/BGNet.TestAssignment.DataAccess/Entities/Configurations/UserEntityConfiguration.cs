using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BGNet.TestAssignment.DataAccess.Entities.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    #region -- IEntityTypeConfiguration implementation --

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Username).IsUnique();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
    }

    #endregion
}
