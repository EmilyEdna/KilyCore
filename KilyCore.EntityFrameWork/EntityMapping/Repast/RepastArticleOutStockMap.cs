using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastArticleOutStockMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/9 11:32:27
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Repast
{
    public class RepastArticleOutStockMap : IEntityTypeConfiguration<RepastArticleOutStock>
    {
        public void Configure(EntityTypeBuilder<RepastArticleOutStock> builder)
        {
            builder.ToTable(typeof(RepastArticleOutStock).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.OutStockTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
