using System;
using System.Collections.Generic;
using System.Text;
using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemRoleLvMap : IEntityTypeConfiguration<SystemRoleLevel>
    {
        public void Configure(EntityTypeBuilder<SystemRoleLevel> builder)
        {
            builder.ToTable(typeof(SystemRoleLevel).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.LvName).HasMaxLength(50);
        }
    }
}
