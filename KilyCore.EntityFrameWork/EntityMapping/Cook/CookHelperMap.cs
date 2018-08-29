using KilyCore.EntityFrameWork.Model.Cook;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookHelperMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/29 11:03:58
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Cook
{
    public class CookHelperMap : IEntityTypeConfiguration<CookHelper>
    {
        public void Configure(EntityTypeBuilder<CookHelper> builder)
        {
            builder.ToTable(typeof(CookHelper).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class CookAgreeMap : IEntityTypeConfiguration<CookAgree>
    {
        public void Configure(EntityTypeBuilder<CookAgree> builder)
        {
            builder.ToTable(typeof(CookAgree).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
