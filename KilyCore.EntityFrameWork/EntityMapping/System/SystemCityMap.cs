using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemCityMap : IEntityTypeConfiguration<SystemCity>
    {
        public void Configure(EntityTypeBuilder<SystemCity> builder)
        {
            builder.ToTable(typeof(SystemCity).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
