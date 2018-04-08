using KilyCore.EntityFrameWork.Model.Dining;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Dining
{
    public class DiningMenuMap : IEntityTypeConfiguration<DiningMenu>
    {
        public void Configure(EntityTypeBuilder<DiningMenu> builder)
        {
            builder.ToTable(typeof(DiningMenu).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
