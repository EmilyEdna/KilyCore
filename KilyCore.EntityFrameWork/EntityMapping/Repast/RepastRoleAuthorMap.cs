using KilyCore.EntityFrameWork.Model.Repast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.EntityMapping.Dining
{
    public class RepastRoleAuthorMap : IEntityTypeConfiguration<RepastRoleAuthor>
    {
        public void Configure(EntityTypeBuilder<RepastRoleAuthor> builder)
        {
            builder.ToTable(typeof(RepastRoleAuthor).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
