using KilyCore.EntityFrameWork.Model.Cook;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

#region << 版 本 注 释 >>

/*----------------------------------------------------------------
* 类 名 称 ：CookInfoMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Cook
* 机器名称 ：EMILY
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 14:14:27
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/

#endregion << 版 本 注 释 >>

namespace KilyCore.EntityFrameWork.EntityMapping.Cook
{
    public class CookInfoMap : IEntityTypeConfiguration<CookInfo>
    {
        public void Configure(EntityTypeBuilder<CookInfo> builder)
        {
            builder.ToTable(typeof(CookInfo).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ExpiredDay).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.Birthday).HasColumnType(typeof(DateTime).Name);
        }
    }
}