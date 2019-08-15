using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemOrderScoreMap : IEntityTypeConfiguration<SystemOrderScore>
    {
        public void Configure(EntityTypeBuilder<SystemOrderScore> builder)
        {
            builder.ToTable(typeof(SystemOrderScore).Name);
            builder.Property(t => t.ScoreTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
