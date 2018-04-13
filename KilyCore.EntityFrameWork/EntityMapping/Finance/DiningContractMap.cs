using KilyCore.EntityFrameWork.Model.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Finance
{
    public class DiningContractMap : IEntityTypeConfiguration<DiningContract>
    {
        public void Configure(EntityTypeBuilder<DiningContract> builder)
        {
            builder.ToTable(typeof(DiningContract).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
