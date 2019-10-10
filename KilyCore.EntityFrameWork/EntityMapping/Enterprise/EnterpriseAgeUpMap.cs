using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseAgeUpMap : IEntityTypeConfiguration<EnterpriseAgeUp>
    {
        public void Configure(EntityTypeBuilder<EnterpriseAgeUp> builder)
        {
            builder.ToTable(typeof(EnterpriseAgeUp).Name);
            builder.HasKey(t => t.Id);
        }
    }
}