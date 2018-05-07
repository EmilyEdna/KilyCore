using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemPresonMap : IEntityTypeConfiguration<SystemPreson>
    {
        public void Configure(EntityTypeBuilder<SystemPreson> builder)
        {
            builder.ToTable(typeof(SystemPreson).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
