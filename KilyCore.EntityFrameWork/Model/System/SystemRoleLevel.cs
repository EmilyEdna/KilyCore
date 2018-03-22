using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 权限等级表
    /// </summary>
    public class SystemRoleLevel :BaseEntity
    {
        /// <summary>
        /// 等级名称
        /// </summary>
        public string LvName { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public RoleEnum RoleLv { get; set; }
    }
}
