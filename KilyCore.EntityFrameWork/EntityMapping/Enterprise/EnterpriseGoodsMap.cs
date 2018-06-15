using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseGoodsMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/13 15:18:27
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseGoodsMap : IEntityTypeConfiguration<EnterpriseGoods>
    {
        public void Configure(EntityTypeBuilder<EnterpriseGoods> builder)
        {
            builder.ToTable(typeof(EnterpriseGoods).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class EnterpriseGoodsStockMap : IEntityTypeConfiguration<EnterpriseGoodsStock>
    {
        public void Configure(EntityTypeBuilder<EnterpriseGoodsStock> builder)
        {
            builder.ToTable(typeof(EnterpriseGoodsStock).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class EnterpriseGoodsStockAttachMap : IEntityTypeConfiguration<EnterpriseGoodsStockAttach>
    {
        public void Configure(EntityTypeBuilder<EnterpriseGoodsStockAttach> builder)
        {
            builder.ToTable(typeof(EnterpriseGoodsStockAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
