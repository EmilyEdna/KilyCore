using KilyCore.EntityFrameWork.Model.Cook;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookFood
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/29 11:11:03
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Cook
{
    public class CookFoodMap : IEntityTypeConfiguration<CookFood>
    {
        public void Configure(EntityTypeBuilder<CookFood> builder)
        {
            builder.ToTable(typeof(CookFood).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.BuyTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
