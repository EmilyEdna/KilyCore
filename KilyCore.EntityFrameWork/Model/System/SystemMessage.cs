using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：SystemMessage
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.System
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/20 16:40:29
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 系统消息表
    /// </summary>
    public class SystemMessage : BaseEntity
    {
        /// <summary>
        /// 企业的Id
        /// </summary>
        public virtual Guid? CompanyId { get; set; }
        /// <summary>
        /// 推送时间
        /// </summary>
        public virtual DateTime? ReleaseTime { get; set; }
        /// <summary>
        /// 消息名称
        /// </summary>
        public virtual string MsgName { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public virtual string MsgContent { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual string TypePath { get; set; }
    }
}
