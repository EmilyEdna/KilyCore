using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：SystemNewsMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.System
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/10/26 11:43:33
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemNewsMap : IEntityTypeConfiguration<SystemNews>
    {
        public void Configure(EntityTypeBuilder<SystemNews> builder)
        {
            builder.ToTable(typeof(SystemNews).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ReleaseDate).HasColumnType(typeof(DateTime).Name);
        }
    }
}
