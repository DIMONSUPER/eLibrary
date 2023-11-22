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
    }

    #endregion
}
