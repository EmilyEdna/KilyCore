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
    public class EnterpriseInfoMap : IEntityTypeConfiguration<EnterpriseInfo>
    {
        public void Configure(EntityTypeBuilder<EnterpriseInfo> builder)
        {
            builder.ToTable(typeof(EnterpriseInfo).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CardExpiredDate).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.CompanyAccount).IsRequired();
            builder.Property(t => t.PassWord).IsRequired();
            builder.Property(t => t.CompanyName).IsRequired();
        }
    }
}
