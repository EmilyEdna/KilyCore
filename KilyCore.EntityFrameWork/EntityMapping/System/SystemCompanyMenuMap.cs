using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemCompanyMenuMap : IEntityTypeConfiguration<SystemCompanyMenu>
    {
        public void Configure(EntityTypeBuilder<SystemCompanyMenu> builder)
        {
            builder.ToTable(typeof(SystemCompanyMenu).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
