using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseEnvironmentMap : IEntityTypeConfiguration<EnterpriseEnvironment>
    {
        public void Configure(EntityTypeBuilder<EnterpriseEnvironment> builder)
        {
            builder.ToTable(typeof(EnterpriseEnvironment).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.RecordTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class EnterpriseEnvironmentAttachMap : IEntityTypeConfiguration<EnterpriseEnvironmentAttach>
    {
        public void Configure(EntityTypeBuilder<EnterpriseEnvironmentAttach> builder)
        {
            builder.ToTable(typeof(EnterpriseEnvironmentAttach).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.RecordTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
