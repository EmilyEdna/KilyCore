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
    public class SystemAdminMap: IEntityTypeConfiguration<SystemAdmin>
    {
        public void Configure(EntityTypeBuilder<SystemAdmin> builder)
        {
            builder.ToTable(typeof(SystemAdmin).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Account).IsRequired();
            builder.Property(t => t.PassWord).IsRequired();
        }
    }
}
