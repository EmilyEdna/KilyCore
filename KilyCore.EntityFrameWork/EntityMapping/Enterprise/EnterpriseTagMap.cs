using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseTagMap : IEntityTypeConfiguration<EnterpriseTag>
    {
        public void Configure(EntityTypeBuilder<EnterpriseTag> builder)
        {
            builder.ToTable(typeof(EnterpriseTag).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
