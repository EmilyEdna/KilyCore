using KilyCore.EntityFrameWork.Model.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Finance
{
    public class SystemAdminAttachMap : IEntityTypeConfiguration<SystemAdminAttach>
    {
        public void Configure(EntityTypeBuilder<SystemAdminAttach> builder)
        {
            builder.ToTable(typeof(SystemAdminAttach).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.StartTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.EndTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.Money).HasColumnType("decimal(18, 2)");
        }
    }
}
