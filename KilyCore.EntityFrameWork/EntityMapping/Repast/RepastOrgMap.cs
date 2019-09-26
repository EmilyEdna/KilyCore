using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Repast
{
    public class RepastOrgMap : IEntityTypeConfiguration<RepastOrg>
    {
        public void Configure(EntityTypeBuilder<RepastOrg> builder)
        {
            builder.ToTable(typeof(RepastOrg).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
