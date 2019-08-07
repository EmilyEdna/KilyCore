using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseTagUseRecordMap : IEntityTypeConfiguration<EnterpriseTagUseRecord>
    {
        public void Configure(EntityTypeBuilder<EnterpriseTagUseRecord> builder)
        {
            builder.ToTable(typeof(EnterpriseTagUseRecord).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
