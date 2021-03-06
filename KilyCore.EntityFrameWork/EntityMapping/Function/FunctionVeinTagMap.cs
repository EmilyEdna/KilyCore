﻿using System;
using System.Collections.Generic;
using System.Text;
using KilyCore.EntityFrameWork.Model.Function;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.EntityMapping.Function
{
    public class FunctionVeinTagMap : IEntityTypeConfiguration<FunctionVeinTag>
    {
        public void Configure(EntityTypeBuilder<FunctionVeinTag> builder)
        {
            builder.ToTable(typeof(FunctionVeinTag).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.AcceptTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class FunctionVeinTagAttachMap : IEntityTypeConfiguration<FunctionVeinTagAttach>
    {
        public void Configure(EntityTypeBuilder<FunctionVeinTagAttach> builder)
        {
            builder.ToTable(typeof(FunctionVeinTagAttach).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.AcceptTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
