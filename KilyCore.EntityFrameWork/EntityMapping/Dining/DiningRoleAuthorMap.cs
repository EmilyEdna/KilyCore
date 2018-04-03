using KilyCore.EntityFrameWork.Model.Dining;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Dining
{
    public class DiningRoleAuthorMap : IEntityTypeConfiguration<DiningRoleAuthor>
    {
        public void Configure(EntityTypeBuilder<DiningRoleAuthor> builder)
        {
            builder.ToTable(typeof(DiningRoleAuthor).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
