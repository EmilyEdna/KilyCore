using KilyCore.EntityFrameWork.Model.Function;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
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
    public class FunctionDisDictionaryMap : IEntityTypeConfiguration<FunctionDisDictionary>
    {
        public void Configure(EntityTypeBuilder<FunctionDisDictionary> builder)
        {
            builder.ToTable(typeof(FunctionDisDictionary).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
