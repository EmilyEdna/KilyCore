using KilyCore.EntityFrameWork.Model.Govt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtPatrolCategoryMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/25 11:14:25
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Govt
{
    public class GovtPatrolCategoryMap : IEntityTypeConfiguration<GovtPatrolCategory>
    {
        public void Configure(EntityTypeBuilder<GovtPatrolCategory> builder)
        {
            builder.ToTable(typeof(GovtPatrolCategory).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class GovtPatrolCategoryAttachMap : IEntityTypeConfiguration<GovtPatrolCategoryAttach>
    {
        public void Configure(EntityTypeBuilder<GovtPatrolCategoryAttach> builder)
        {
            builder.ToTable(typeof(GovtPatrolCategoryAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
