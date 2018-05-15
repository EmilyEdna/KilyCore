using System;
using System.Collections.Generic;
using System.Text;
using KilyCore.EntityFrameWork.Model.Function;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KilyCore.EntityFrameWork.EntityMapping.Function
{
    public class FunctionVeinTagMap : IEntityTypeConfiguration<FunctionVeinTag>
    {
        public void Configure(EntityTypeBuilder<FunctionVeinTag> builder)
        {
            builder.ToTable(typeof(FunctionVeinTag).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.AcceptTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
