using KilyCore.EntityFrameWork.Model.Govt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Govt
{
    public class GovtNetPatrolLogMap : IEntityTypeConfiguration<GovtNetPatrolLog>
    {
        public void Configure(EntityTypeBuilder<GovtNetPatrolLog> builder)
        {
            builder.ToTable(typeof(GovtNetPatrolLog).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.RecordTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
