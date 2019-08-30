using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Repast
{
    public class RepastUnitInsMap : IEntityTypeConfiguration<RepastUnitIns>
    {
        public void Configure(EntityTypeBuilder<RepastUnitIns> builder)
        {
            builder.ToTable(typeof(RepastUnitIns).Name);
            builder.Property(t => t.InsTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class RepastUnitInsRecordMap : IEntityTypeConfiguration<RepastUnitInsRecord>
    {
        public void Configure(EntityTypeBuilder<RepastUnitInsRecord> builder)
        {
            builder.ToTable(typeof(RepastUnitInsRecord).Name);
            builder.Property(t => t.InsTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
