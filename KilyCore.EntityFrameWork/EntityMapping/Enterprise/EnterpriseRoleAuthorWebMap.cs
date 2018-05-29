using System;
using System.Collections.Generic;
using System.Text;
using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseRoleAuthorWebMap : IEntityTypeConfiguration<EnterpriseRoleAuthorWeb>
    {
        public void Configure(EntityTypeBuilder<EnterpriseRoleAuthorWeb> builder)
        {
            builder.ToTable(typeof(EnterpriseRoleAuthorWeb).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
