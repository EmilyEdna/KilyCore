using KilyCore.EntityFrameWork.Model.Govt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtInfoMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/6 15:25:17
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Govt
{
    public class GovtInfoMap : IEntityTypeConfiguration<GovtInfo>
    {
        public void Configure(EntityTypeBuilder<GovtInfo> builder)
        {
            builder.ToTable(typeof(GovtInfo).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
