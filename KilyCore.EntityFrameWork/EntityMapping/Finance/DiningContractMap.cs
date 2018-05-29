using KilyCore.EntityFrameWork.Model.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
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
