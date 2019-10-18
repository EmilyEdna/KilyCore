using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterprisePackCodeBindMap : IEntityTypeConfiguration<EnterprisePackCodeBind>
    {
        public void Configure(EntityTypeBuilder<EnterprisePackCodeBind> builder)
        {
            builder.ToTable(typeof(EnterprisePackCodeBind).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.PacTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
