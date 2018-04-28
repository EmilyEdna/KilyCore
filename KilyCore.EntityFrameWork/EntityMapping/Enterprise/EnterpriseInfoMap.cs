using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseInfoMap : IEntityTypeConfiguration<EnterpriseInfo>
    {
        public void Configure(EntityTypeBuilder<EnterpriseInfo> builder)
        {
            builder.ToTable(typeof(EnterpriseInfo).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CompanyAccount).IsRequired();
            builder.Property(t => t.PassWord).IsRequired();
            builder.Property(t => t.CompanyName).IsRequired();
        }
    }
}
