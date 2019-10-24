using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemNetServiceMap : IEntityTypeConfiguration<SystemNetService>
    {
        public void Configure(EntityTypeBuilder<SystemNetService> builder)
        {
            builder.ToTable(typeof(SystemNetService).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ETime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.STime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
