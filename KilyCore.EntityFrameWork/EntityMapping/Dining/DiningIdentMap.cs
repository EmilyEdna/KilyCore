using KilyCore.EntityFrameWork.Model.Dining;
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
    public class DiningIdentMap : IEntityTypeConfiguration<DiningIdent>
    {
        public void Configure(EntityTypeBuilder<DiningIdent> builder)
        {
            builder.ToTable(typeof(DiningIdent).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.IdentStartTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.IdentEndTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class DiningIdentAttachMap : IEntityTypeConfiguration<DiningIdentAttach>
    {
        public void Configure(EntityTypeBuilder<DiningIdentAttach> builder)
        {
            builder.ToTable(typeof(DiningIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
