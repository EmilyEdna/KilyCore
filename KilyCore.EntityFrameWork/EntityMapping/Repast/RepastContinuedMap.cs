using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastContinuedMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Repast
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/8/1 10:32:51
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Repast
{
    public class RepastContinuedMap : IEntityTypeConfiguration<RepastContinued>
    {
        public void Configure(EntityTypeBuilder<RepastContinued> builder)
        {
            builder.ToTable(typeof(RepastContinued).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class RepastUpLevelMap : IEntityTypeConfiguration<RepastUpLevel>
    {
        public void Configure(EntityTypeBuilder<RepastUpLevel> builder)
        {
            builder.ToTable(typeof(RepastUpLevel).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
