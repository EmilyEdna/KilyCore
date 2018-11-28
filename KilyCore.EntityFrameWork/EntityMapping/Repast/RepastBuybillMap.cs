using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastBuybillMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/3 11:13:06
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Repast
{
    public class RepastBuybillMap : IEntityTypeConfiguration<RepastBuybill>
    {
        public void Configure(EntityTypeBuilder<RepastBuybill> builder)
        {
            builder.ToTable(typeof(RepastBuybill).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.OrderTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
