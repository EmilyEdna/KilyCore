using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemPersonOffMap : IEntityTypeConfiguration<SystemPersonOff>
    {
        public void Configure(EntityTypeBuilder<SystemPersonOff> builder)
        {
            builder.ToTable(typeof(SystemPersonOff).Name);
        }
    }
}
