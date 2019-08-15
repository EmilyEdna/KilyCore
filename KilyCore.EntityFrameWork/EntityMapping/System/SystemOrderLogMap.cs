using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemOrderLogMap : IEntityTypeConfiguration<SystemOrderLog>
    {
        public void Configure(EntityTypeBuilder<SystemOrderLog> builder)
        {
            builder.ToTable(typeof(SystemOrderLog).Name);
            builder.Property(t => t.HandlerTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
