﻿using KilyCore.EntityFrameWork.Model.System;
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
    public class SystemStayContractMap : IEntityTypeConfiguration<SystemStayContract>
    {
        public void Configure(EntityTypeBuilder<SystemStayContract> builder)
        {
            builder.ToTable(typeof(SystemStayContract).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.EndTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.TryStarDate).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.TryEndDate).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.ActualPrice).HasColumnType("decimal(18,2)");
        }
    }
}
