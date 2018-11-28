using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseBuyerMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/9 14:34:39
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseBuyerMap : IEntityTypeConfiguration<EnterpriseBuyer>
    {
        public void Configure(EntityTypeBuilder<EnterpriseBuyer> builder)
        {
            builder.ToTable(typeof(EnterpriseBuyer).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.GetGoodsTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.ProTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
