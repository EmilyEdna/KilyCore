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
    public class EnterpriseDrugMap : IEntityTypeConfiguration<EnterpriseDrug>
    {
        public void Configure(EntityTypeBuilder<EnterpriseDrug> builder)
        {
            builder.ToTable(typeof(EnterpriseDrug).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.PlantTime).HasColumnType(typeof(DateTime).Name);
        }
    }
}
