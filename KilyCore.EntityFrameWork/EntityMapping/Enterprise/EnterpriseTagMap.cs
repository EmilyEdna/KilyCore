using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
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
    public class EnterpriseVeinTagMap : IEntityTypeConfiguration<EnterpriseVeinTag>
    {
        public void Configure(EntityTypeBuilder<EnterpriseVeinTag> builder)
        {
            builder.ToTable(typeof(EnterpriseVeinTag).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.AcceptTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
