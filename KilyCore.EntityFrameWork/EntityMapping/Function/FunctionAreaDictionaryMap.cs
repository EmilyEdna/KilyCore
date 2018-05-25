using KilyCore.EntityFrameWork.Model.Function;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Function
{
    public class FunctionAreaDictionaryMap : IEntityTypeConfiguration<FunctionAreaDictionary>
    {
        public void Configure(EntityTypeBuilder<FunctionAreaDictionary> builder)
        {
            builder.ToTable(typeof(FunctionAreaDictionary).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
