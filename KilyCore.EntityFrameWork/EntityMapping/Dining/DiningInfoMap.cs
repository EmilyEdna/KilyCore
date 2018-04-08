using KilyCore.EntityFrameWork.Model.Dining;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Dining
{
    public class DiningInfoMap: IEntityTypeConfiguration<DiningInfo>
    {
        public void Configure(EntityTypeBuilder<DiningInfo> builder)
        {
            builder.ToTable(typeof(DiningInfo).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Account).IsRequired();
            builder.Property(t => t.PassWord).IsRequired();
            builder.Property(t => t.MerchantName).IsRequired();
        }
    }
}
