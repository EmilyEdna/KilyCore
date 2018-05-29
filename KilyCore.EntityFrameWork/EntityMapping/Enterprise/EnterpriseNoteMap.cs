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
    public class EnterpriseNoteMap : IEntityTypeConfiguration<EnterpriseNote>
    {
        public void Configure(EntityTypeBuilder<EnterpriseNote> builder)
        {
            builder.ToTable(typeof(EnterpriseNote).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t=>t.ResultTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
