using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

#region << 版 本 注 释 >>

/*----------------------------------------------------------------
* 项目名称 ：KilyCore.EntityFrameWork.EntityMapping.Enterprise
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Enterprise
* 机器名称 ：EMILY
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/21 15:03:54
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/

#endregion << 版 本 注 释 >>

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseBoxingMap : IEntityTypeConfiguration<EnterpriseBoxing>
    {
        public void Configure(EntityTypeBuilder<EnterpriseBoxing> builder)
        {
            builder.ToTable(typeof(EnterpriseBoxing).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.BoxTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}