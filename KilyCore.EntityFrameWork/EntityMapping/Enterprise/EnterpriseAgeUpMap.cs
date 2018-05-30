using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseAgeUpMap : IEntityTypeConfiguration<EnterpriseAgeUp>
    {
        public void Configure(EntityTypeBuilder<EnterpriseAgeUp> builder)
        {
            builder.ToTable(typeof(EnterpriseAgeUp).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
