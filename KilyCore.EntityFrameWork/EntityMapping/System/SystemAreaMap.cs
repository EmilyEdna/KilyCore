using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemAreaMap : IEntityTypeConfiguration<SystemArea>
    {
        public void Configure(EntityTypeBuilder<SystemArea> builder)
        {
            builder.ToTable(typeof(SystemArea).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
