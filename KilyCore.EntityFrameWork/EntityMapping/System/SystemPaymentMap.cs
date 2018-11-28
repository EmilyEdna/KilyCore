using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemPaymentMap : IEntityTypeConfiguration<SystemPayment>
    {
        public void Configure(EntityTypeBuilder<SystemPayment> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.PayTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.Paymoney).HasColumnType("decimal(18, 2)");
        }
    }
}
