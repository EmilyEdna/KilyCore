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
    public class SystemDictionaryMap : IEntityTypeConfiguration<FunctionDictionary>
    {
        public void Configure(EntityTypeBuilder<FunctionDictionary> builder)
        {
            builder.ToTable(typeof(FunctionDictionary).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
