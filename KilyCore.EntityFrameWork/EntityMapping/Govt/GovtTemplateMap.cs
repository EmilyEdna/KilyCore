using KilyCore.EntityFrameWork.Model.Govt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：KilyCore.EntityFrameWork.EntityMapping.Govt
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/28 9:58:09
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Govt
{
    public class GovtTemplateMap : IEntityTypeConfiguration<GovtTemplate>
    {
        public void Configure(EntityTypeBuilder<GovtTemplate> builder)
        {
            builder.ToTable(typeof(GovtTemplate).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class GovtTemplateChildMap : IEntityTypeConfiguration<GovtTemplateChild>
    {
        public void Configure(EntityTypeBuilder<GovtTemplateChild> builder)
        {
            builder.ToTable(typeof(GovtTemplateChild).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
