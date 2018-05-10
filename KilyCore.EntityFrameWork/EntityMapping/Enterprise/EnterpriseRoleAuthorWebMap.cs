using System;
using System.Collections.Generic;
using System.Text;
using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseRoleAuthorWebMap : IEntityTypeConfiguration<EnterpriseRoleAuthorWeb>
    {
        public void Configure(EntityTypeBuilder<EnterpriseRoleAuthorWeb> builder)
        {
            builder.ToTable(typeof(EnterpriseRoleAuthorWeb).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
