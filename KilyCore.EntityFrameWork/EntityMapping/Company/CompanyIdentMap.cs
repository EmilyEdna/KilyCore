using KilyCore.EntityFrameWork.Model.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Company
{
    public class CompanyIdentMap : IEntityTypeConfiguration<CompanyIdent>
    {
        public void Configure(EntityTypeBuilder<CompanyIdent> builder)
        {
            builder.ToTable(typeof(CompanyIdent).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class CompanyCirculationIdentAttachMap : IEntityTypeConfiguration<CompanyCirculationIdentAttach>
    {
        public void Configure(EntityTypeBuilder<CompanyCirculationIdentAttach> builder)
        {
            builder.ToTable(typeof(CompanyCirculationIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class CompanyPlantIdentAttachMap : IEntityTypeConfiguration<CompanyPlantIdentAttach>
    {
        public void Configure(EntityTypeBuilder<CompanyPlantIdentAttach> builder)
        {
            builder.ToTable(typeof(CompanyPlantIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class CompanyProductionIdentAttachMap : IEntityTypeConfiguration<CompanyProductionIdentAttach>
    {
        public void Configure(EntityTypeBuilder<CompanyProductionIdentAttach> builder)
        {
            builder.ToTable(typeof(CompanyProductionIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
    public class CompanyOtherIdentAttachMap : IEntityTypeConfiguration<CompanyOtherIdentAttach>
    {
        public void Configure(EntityTypeBuilder<CompanyOtherIdentAttach> builder)
        {
            builder.ToTable(typeof(CompanyOtherIdentAttach).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
