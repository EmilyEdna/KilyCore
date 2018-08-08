using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastInStorageMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/7 11:51:25
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Repast
{
    public class RepastInStorageMap : IEntityTypeConfiguration<RepastInStorage>
    {
        public void Configure(EntityTypeBuilder<RepastInStorage> builder)
        {
            builder.ToTable(typeof(RepastInStorage).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.SuppTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.PrePrice).HasColumnType("decimal");
            builder.Property(t => t.ToPrice).HasColumnType("decimal");
        }
    }
}
