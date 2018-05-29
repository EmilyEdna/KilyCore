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
    public class EnterpriseTagApplyMap : IEntityTypeConfiguration<EnterpriseTagApply>
    {
        public void Configure(EntityTypeBuilder<EnterpriseTagApply> builder)
        {
            builder.ToTable(typeof(EnterpriseTagApply).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ApplyMoney).HasColumnType("decimal(18,3)");
        }
    }
}
