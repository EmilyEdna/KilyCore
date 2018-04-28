using KilyCore.EntityFrameWork.Model.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Finance
{
    public class StayContractMap : IEntityTypeConfiguration<SystemStayContract>
    {
        public void Configure(EntityTypeBuilder<SystemStayContract> builder)
        {
            builder.ToTable(typeof(SystemStayContract).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
