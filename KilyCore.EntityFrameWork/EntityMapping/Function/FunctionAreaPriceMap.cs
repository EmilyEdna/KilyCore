using System;
using System.Collections.Generic;
using System.Text;
using KilyCore.EntityFrameWork.Model.Function;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KilyCore.EntityFrameWork.EntityMapping.Function
{
    public class FunctionAreaPriceMap : IEntityTypeConfiguration<FunctionAreaPrice>
    {
        public void Configure(EntityTypeBuilder<FunctionAreaPrice> builder)
        {
            builder.ToTable(typeof(FunctionAreaPrice).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ProvincePrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.CityPrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.AreaPrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TownPrice).HasColumnType("decimal(18,2)");
        }
    }
}
