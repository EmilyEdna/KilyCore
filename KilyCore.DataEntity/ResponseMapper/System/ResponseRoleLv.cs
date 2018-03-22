using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.System
{
    /// <summary>
    /// 权限等级
    /// </summary>
    public class ResponseRoleLv
    {
        public Guid Id { get; set; }
        public String LvName { get; set; }
        public RoleEnum RoleLv { get; set; }
    }
}
