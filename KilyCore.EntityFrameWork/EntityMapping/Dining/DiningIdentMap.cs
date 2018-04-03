using KilyCore.EntityFrameWork.Model.Dining;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Dining
{
    public class DiningIdentMap : IEntityTypeConfiguration<DiningIdent>
    {
        public void Configure(EntityTypeBuilder<DiningIdent> builder)
        {
            builder.ToTable(typeof(DiningIdent).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class DiningIdentAttachMap : IEntityTypeConfiguration<DiningIdentAttach>
    {
        public void Configure(EntityTypeBuilder<DiningIdentAttach> builder)
        {
            builder.ToTable(typeof(DiningIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
