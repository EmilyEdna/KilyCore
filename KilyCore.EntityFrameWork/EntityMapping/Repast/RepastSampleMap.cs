using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastSampleMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/13 11:50:36
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Repast
{
    public class RepastSampleMap: IEntityTypeConfiguration<RepastSample>
    {
        public void Configure(EntityTypeBuilder<RepastSample> builder)
        {
            builder.ToTable(typeof(RepastSample).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.SampleTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class RepastDrawMap: IEntityTypeConfiguration<RepastDraw>
    {
        public void Configure(EntityTypeBuilder<RepastDraw> builder)
        {
            builder.ToTable(typeof(RepastDraw).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.DrawTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class RepastDuckMap : IEntityTypeConfiguration<RepastDuck>
    {
        public void Configure(EntityTypeBuilder<RepastDuck> builder)
        {
            builder.ToTable(typeof(RepastDuck).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.HandleTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
