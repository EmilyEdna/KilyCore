using KilyCore.EntityFrameWork.Model.Function;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.EntityMapping.Function
{
    public class SystemDictionaryMap : IEntityTypeConfiguration<FunctionDictionary>
    {
        public void Configure(EntityTypeBuilder<FunctionDictionary> builder)
        {
            builder.ToTable(typeof(FunctionDictionary).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
