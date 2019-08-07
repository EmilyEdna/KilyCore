using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastInfoUserMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Repast
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/17 14:28:59
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Repast
{
    public class RepastInfoUserMap : IEntityTypeConfiguration<RepastInfoUser>
    {
        public void Configure(EntityTypeBuilder<RepastInfoUser> builder)
        {
            builder.ToTable(typeof(RepastInfoUser).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Account).IsRequired();
            builder.Property(t => t.ExpiredTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.PassWord).IsRequired();
        }
    }
}
