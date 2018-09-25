using KilyCore.EntityFrameWork.Model.Govt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtMovePatrolMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/25 16:20:17
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Govt
{
    public class GovtMovePatrolMap : IEntityTypeConfiguration<GovtMovePatrol>
    {
        public void Configure(EntityTypeBuilder<GovtMovePatrol> builder)
        {
            builder.ToTable(typeof(GovtMovePatrol).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.PatrolTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
