using KilyCore.EntityFrameWork.Model.Cook;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#region << 版 本 注 释 >>

/*----------------------------------------------------------------
* 类 名 称 ：CookRoleAuthorMap
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Cook
* 机器名称 ：EMILY
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 15:13:26
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/

#endregion << 版 本 注 释 >>

namespace KilyCore.EntityFrameWork.EntityMapping.Cook
{
    public class CookRoleAuthorMap : IEntityTypeConfiguration<CookRoleAuthor>
    {
        public void Configure(EntityTypeBuilder<CookRoleAuthor> builder)
        {
            builder.ToTable(typeof(CookRoleAuthor).Name);
            builder.HasKey(t => t.Id);
        }
    }
}