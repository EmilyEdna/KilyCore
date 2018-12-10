using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.System
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/10 14:40:43
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemPayInfoMap: IEntityTypeConfiguration<SystemPayInfo>
    {
        public void Configure(EntityTypeBuilder<SystemPayInfo> builder)
        {
            builder.ToTable(typeof(SystemPayInfo).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
