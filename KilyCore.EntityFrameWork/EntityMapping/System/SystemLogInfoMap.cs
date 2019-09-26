using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemLogInfoMap : IEntityTypeConfiguration<SystemLogInfo>
    {
        public void Configure(EntityTypeBuilder<SystemLogInfo> builder)
        {
            builder.ToTable(typeof(SystemLogInfo).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
