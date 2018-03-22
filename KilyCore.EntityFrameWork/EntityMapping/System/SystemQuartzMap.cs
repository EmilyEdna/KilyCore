using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemQuartzMap : IEntityTypeConfiguration<SystemQuartz>
    {
        public void Configure(EntityTypeBuilder<SystemQuartz> builder)
        {
            builder.ToTable(typeof(SystemQuartz).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
