using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemTownMap : IEntityTypeConfiguration<SystemTown>
    {
        public void Configure(EntityTypeBuilder<SystemTown> builder)
        {
            builder.ToTable(typeof(SystemTown).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
