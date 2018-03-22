using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemAuditMap : IEntityTypeConfiguration<SystemAudit>
    {
        public void Configure(EntityTypeBuilder<SystemAudit> builder)
        {
            builder.ToTable(typeof(SystemAudit).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TableName).IsRequired();
            builder.Property(t => t.TableId).IsRequired();
        }
    }
}
