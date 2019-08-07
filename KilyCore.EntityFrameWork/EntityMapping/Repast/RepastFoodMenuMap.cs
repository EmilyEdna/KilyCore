using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Repast
{
    public class RepastFoodMenuMap: IEntityTypeConfiguration<RepastFoodMenu>
    {
        public void Configure(EntityTypeBuilder<RepastFoodMenu> builder)
        {
            builder.ToTable(typeof(RepastFoodMenu).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.UpTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
