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
    public class SystemQuartzMap : IEntityTypeConfiguration<SystemQuartz>
    {
        public void Configure(EntityTypeBuilder<SystemQuartz> builder)
        {
            builder.ToTable(typeof(SystemQuartz).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.StartTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.EndTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
