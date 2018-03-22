using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemRoleAuthorMap : IEntityTypeConfiguration<SystemRoleAuthor>
    {
        public void Configure(EntityTypeBuilder<SystemRoleAuthor> builder)
        {
            builder.ToTable(typeof(SystemRoleAuthor).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
