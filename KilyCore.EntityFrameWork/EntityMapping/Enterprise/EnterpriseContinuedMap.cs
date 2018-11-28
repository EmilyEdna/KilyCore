using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseContinuedMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/3 14:23:23
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseContinuedMap : IEntityTypeConfiguration<EnterpriseContinued>
    {
        public void Configure(EntityTypeBuilder<EnterpriseContinued> builder)
        {
            builder.ToTable(typeof(EnterpriseContinued).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class EnterpriseUpLevelMap : IEntityTypeConfiguration<EnterpriseUpLevel>
    {
        public void Configure(EntityTypeBuilder<EnterpriseUpLevel> builder)
        {
            builder.ToTable(typeof(EnterpriseUpLevel).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
