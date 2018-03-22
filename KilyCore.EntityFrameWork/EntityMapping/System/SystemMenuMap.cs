using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemMenuMap : IEntityTypeConfiguration<SystemMenu>
    {
        public void Configure(EntityTypeBuilder<SystemMenu> builder)
        {
            builder.ToTable(typeof(SystemMenu).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
