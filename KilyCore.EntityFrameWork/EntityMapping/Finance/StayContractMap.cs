using KilyCore.EntityFrameWork.Model.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Finance
{
    public class StayContractMap : IEntityTypeConfiguration<StayContract>
    {
        public void Configure(EntityTypeBuilder<StayContract> builder)
        {
            builder.ToTable(typeof(StayContract).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
