using KilyCore.EntityFrameWork.Model.Cook;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookVipMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 11:31:04
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Cook
{
    public class CookVipMap : IEntityTypeConfiguration<CookVip>
    {
        public void Configure(EntityTypeBuilder<CookVip> builder)
        {
            builder.ToTable(typeof(CookVip).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.StartTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.EndTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
