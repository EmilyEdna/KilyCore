using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookVip
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 11:25:49
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Cook
{
    /// <summary>
    /// 厨师会员中心
    /// </summary>
    public class CookVip : CookBase
    {
        /// <summary>
        /// 账号
        /// </summary>
        public virtual string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string PassWord { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public virtual string Photo { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 是否会员
        /// </summary>
        public virtual bool IsVip { get; set; }
        /// <summary>
        /// 开通时间
        /// </summary>
        public virtual DateTime? StartTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public virtual Guid? RoleId { get; set; }
    }
}
