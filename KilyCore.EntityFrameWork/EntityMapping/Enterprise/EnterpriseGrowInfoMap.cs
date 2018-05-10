using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseGrowInfoMap : IEntityTypeConfiguration<EnterpriseGrowInfo>
    {
        public void Configure(EntityTypeBuilder<EnterpriseGrowInfo> builder)
        {
            builder.ToTable(typeof(EnterpriseGrowInfo).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.BuyTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
