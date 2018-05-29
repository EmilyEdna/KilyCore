using KilyCore.EntityFrameWork.Model.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.EntityMapping.Finance
{
    class DiningPaymentMap : IEntityTypeConfiguration<DiningPayment>
    {
        public void Configure(EntityTypeBuilder<DiningPayment> builder)
        {
            builder.ToTable(typeof(DiningPayment).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.OrderMoneySum).HasColumnType("decimal(18, 2)");
            builder.Property(t=>t.Paymoney).HasColumnType("decimal(18, 2)");
            builder.Property(t => t.EnableYearEndTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.PayTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
