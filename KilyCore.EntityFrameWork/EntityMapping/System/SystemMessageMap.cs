using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：SystemMessageMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.System
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/20 17:18:14
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemMessageMap : IEntityTypeConfiguration<SystemMessage>
    {
        public void Configure(EntityTypeBuilder<SystemMessage> builder)
        {
            builder.ToTable(typeof(SystemMessage).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ReleaseTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.HandleTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
