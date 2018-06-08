using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseDeviceMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/8 14:24:46
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseDeviceMap : IEntityTypeConfiguration<EnterpriseDevice>
    {
        public void Configure(EntityTypeBuilder<EnterpriseDevice> builder)
        {
            builder.ToTable(typeof(EnterpriseDevice).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ProductTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class EnterpriseDeviceCleanMap : IEntityTypeConfiguration<EnterpriseDeviceClean>
    {
        public void Configure(EntityTypeBuilder<EnterpriseDeviceClean> builder)
        {
            builder.ToTable(typeof(EnterpriseDeviceClean).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CleanTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class EnterpriseDeviceFixMap : IEntityTypeConfiguration<EnterpriseDeviceFix>
    {
        public void Configure(EntityTypeBuilder<EnterpriseDeviceFix> builder)
        {
            builder.ToTable(typeof(EnterpriseDeviceFix).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.FixTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
