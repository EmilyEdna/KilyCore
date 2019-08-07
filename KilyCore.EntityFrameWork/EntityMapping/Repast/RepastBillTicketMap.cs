using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Repast
{
    public class RepastBillTicketMap : IEntityTypeConfiguration<RepastBillTicket>
    {
        public void Configure(EntityTypeBuilder<RepastBillTicket> builder)
        {
            builder.ToTable(typeof(RepastBillTicket).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.UpTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
