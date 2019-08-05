using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseInviteCodeMap : IEntityTypeConfiguration<EnterpriseInviteCode>
    {
        public void Configure(EntityTypeBuilder<EnterpriseInviteCode> builder)
        {
            builder.ToTable(typeof(EnterpriseInviteCode).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.EffectiveSt).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.EffectiveEt).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.UseTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
