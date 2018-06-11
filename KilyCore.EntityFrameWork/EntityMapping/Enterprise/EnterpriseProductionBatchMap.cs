using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseProductionBatchMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/11 14:52:29
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseProductionBatchMap : IEntityTypeConfiguration<EnterpriseProductionBatch>
    {
        public void Configure(EntityTypeBuilder<EnterpriseProductionBatch> builder)
        {
            builder.ToTable(typeof(EnterpriseProductionBatch).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.StartTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class EnterpriseProductionBatchAttachMap : IEntityTypeConfiguration<EnterpriseProductionBatchAttach>
    {
        public void Configure(EntityTypeBuilder<EnterpriseProductionBatchAttach> builder)
        {
            builder.ToTable(typeof(EnterpriseProductionBatchAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
