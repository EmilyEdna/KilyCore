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
    public class EnterpriseIdentMap : IEntityTypeConfiguration<EnterpriseIdent>
    {
        public void Configure(EntityTypeBuilder<EnterpriseIdent> builder)
        {
            builder.ToTable(typeof(EnterpriseIdent).Name);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.IdentStartTime).HasColumnType(typeof(DateTime).Name);
            builder.Property(t => t.IdentEndTime).HasColumnType(typeof(DateTime).Name);
        }
    }
    public class EnterpriseCirculationIdentAttachMap : IEntityTypeConfiguration<EnterpriseCirculationIdentAttach>
    {
        public void Configure(EntityTypeBuilder<EnterpriseCirculationIdentAttach> builder)
        {
            builder.ToTable(typeof(EnterpriseCirculationIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class EnterprisePlantIdentAttachMap : IEntityTypeConfiguration<EnterprisePlantIdentAttach>
    {
        public void Configure(EntityTypeBuilder<EnterprisePlantIdentAttach> builder)
        {
            builder.ToTable(typeof(EnterprisePlantIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class EnterpriseCultureIdentAttachMap : IEntityTypeConfiguration<EnterpriseCultureIdentAttach>
    {
        public void Configure(EntityTypeBuilder<EnterpriseCultureIdentAttach> builder)
        {
            builder.ToTable(typeof(EnterpriseCultureIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class EnterpriseProductionIdentAttachMap : IEntityTypeConfiguration<EnterpriseProductionIdentAttach>
    {
        public void Configure(EntityTypeBuilder<EnterpriseProductionIdentAttach> builder)
        {
            builder.ToTable(typeof(EnterpriseProductionIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class EnterpriseOtherIdentAttachMap : IEntityTypeConfiguration<EnterpriseOtherIdentAttach>
    {
        public void Configure(EntityTypeBuilder<EnterpriseOtherIdentAttach> builder)
        {
            builder.ToTable(typeof(EnterpriseOtherIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
