using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：SystemAdminAttachMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.System
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/6 10:50:15
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemAdminAttachMap : IEntityTypeConfiguration<SystemAdminAttach>
    {
        public void Configure(EntityTypeBuilder<SystemAdminAttach> builder)
        {
            builder.ToTable(typeof(SystemAdminAttach).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.StartTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.EndTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.Money).HasColumnType("decimal(18, 2)");
        }
    }
}
