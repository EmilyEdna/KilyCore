using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
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
