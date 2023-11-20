﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.DataAccess.Entities.Configurations;

public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    #region -- IEntityTypeConfiguration implementation --

    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
    }

    #endregion
}