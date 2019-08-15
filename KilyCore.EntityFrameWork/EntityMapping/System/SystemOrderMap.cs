using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemOrderMap : IEntityTypeConfiguration<SystemOrder>
    {
        public void Configure(EntityTypeBuilder<SystemOrder> builder)
        {
            builder.ToTable(typeof(SystemOrder).Name);
            builder.Property(t => t.SubmitTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.OrderAccepterTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.ExpireTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.ServicePrice).HasColumnType("decimal(18, 2)");
        }
    }
}
