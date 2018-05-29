using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseGroupUserMap : IEntityTypeConfiguration<EnterpriseGroupUser>
    {
        public void Configure(EntityTypeBuilder<EnterpriseGroupUser> builder)
        {
            builder.ToTable(typeof(EnterpriseGroupUser).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
