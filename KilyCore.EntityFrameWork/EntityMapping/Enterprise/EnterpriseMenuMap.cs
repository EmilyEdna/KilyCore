using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseMenuMap:IEntityTypeConfiguration<EnterpriseMenu>
    {
        public void Configure(EntityTypeBuilder<EnterpriseMenu> builder)
        {
            builder.ToTable(typeof(EnterpriseMenu).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
