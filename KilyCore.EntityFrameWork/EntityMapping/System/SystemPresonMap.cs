﻿using KilyCore.EntityFrameWork.Model.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.EntityMapping.System
{
    public class SystemPresonMap : IEntityTypeConfiguration<SystemPreson>
    {
        public void Configure(EntityTypeBuilder<SystemPreson> builder)
        {
            builder.ToTable(typeof(SystemPreson).Name);
            builder.HasKey(t => t.Id);
        }
    }
}
