using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemProvinceMap : IEntityTypeConfiguration<SystemProvince>
    {
        public void Configure(EntityTypeBuilder<SystemProvince> builder)
        {
            builder.ToTable(typeof(SystemProvince).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
