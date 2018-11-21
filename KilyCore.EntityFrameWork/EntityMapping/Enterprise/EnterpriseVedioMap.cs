using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Enterprise
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/21 11:58:37
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseVedioMap : IEntityTypeConfiguration<EnterpriseVedio>
    {
        public void Configure(EntityTypeBuilder<EnterpriseVedio> builder)
        {
            builder.ToTable(typeof(EnterpriseVedio).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
