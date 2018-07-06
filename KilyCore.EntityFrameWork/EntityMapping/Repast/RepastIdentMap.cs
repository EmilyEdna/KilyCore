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
    public class RepastIdentMap : IEntityTypeConfiguration<RepastIdent>
    {
        public void Configure(EntityTypeBuilder<RepastIdent> builder)
        {
            builder.ToTable(typeof(RepastIdent).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.IdentStartTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.IdentEndTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class DiningIdentAttachMap : IEntityTypeConfiguration<RepastIdentAttach>
    {
        public void Configure(EntityTypeBuilder<RepastIdentAttach> builder)
        {
            builder.ToTable(typeof(RepastIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
