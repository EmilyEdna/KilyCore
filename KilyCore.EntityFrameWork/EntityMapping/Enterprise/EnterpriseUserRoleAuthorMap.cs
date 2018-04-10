﻿using System;
using System.Collections.Generic;
using System.Text;
using KilyCore.EntityFrameWork.Model.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KilyCore.EntityFrameWork.EntityMapping.Enterprise
{
    public class EnterpriseUserRoleAuthorMap : IEntityTypeConfiguration<EnterpriseUserRoleAuthor>
    {
        public void Configure(EntityTypeBuilder<EnterpriseUserRoleAuthor> builder)
        {
            builder.ToTable(typeof(EnterpriseUserRoleAuthor).Name);
            builder.HasKey(t => t.Id);
        }
    }
}