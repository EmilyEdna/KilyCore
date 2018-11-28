using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.EntityMapping.Dining
{
    public class RepastInfoMap: IEntityTypeConfiguration<RepastInfo>
    {
        public void Configure(EntityTypeBuilder<RepastInfo> builder)
        {
            builder.ToTable(typeof(RepastInfo).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Account).IsRequired();
            builder.Property(t => t.PassWord).IsRequired();
            builder.Property(t => t.MerchantName).IsRequired();
            builder.Property(t => t.CardExpiredDate).HasColumnType(typeof(DateTime).Name);
        }
    }
}
