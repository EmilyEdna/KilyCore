using KilyCore.EntityFrameWork.Model.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Company
{
    public class CompnayInfoMap : IEntityTypeConfiguration<CompanyInfo>
    {
        public void Configure(EntityTypeBuilder<CompanyInfo> builder)
        {
            builder.ToTable(typeof(CompanyInfo).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CompanyAccount).IsRequired();
            builder.Property(t => t.PassWord).IsRequired();
        }
    }
}
