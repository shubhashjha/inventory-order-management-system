﻿using Domain.Entities;
using Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Domain.Common.Constants;

namespace Infrastructure.DataAccessManager.EFCore.Configurations;

public class TodoItemConfiguration : BaseEntityConfiguration<TodoItem>
{
    public override void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Description).HasMaxLength(DescriptionConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.TodoId).HasMaxLength(IdConsts.MaxLength).IsRequired(false);

        builder.HasIndex(e => e.Name);
    }
}

