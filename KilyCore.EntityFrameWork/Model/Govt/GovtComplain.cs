using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtComplain
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/28 10:52:44
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 投诉表
    /// </summary>
    public class GovtComplain: GovtBase
    {
        /// <summary>
        /// 投诉人
        /// </summary>
        public virtual string ComplainUser { get; set; }
        /// <summary>
        /// 投诉人电话
        /// </summary>
        public virtual string ComplainUserPhone { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public virtual Guid? CompanyId { get; set; }
        /// <summary>
        ///投诉公司
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public virtual string CompanyType { get; set; }
        /// <summary>
        /// 投诉产品
        /// </summary>
        public virtual string ProductName { get; set; }
        /// <summary>
        /// 投诉时间
        /// </summary>
        public virtual DateTime? ComplainTime { get; set; }
        /// <summary>
        /// 投诉内容
        /// </summary>
        public virtual string ComplainContent { get; set; }
        /// <summary>
        /// 处理内容
        /// </summary>
        public virtual string HandlerContent { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual string Status { get; set; }
        /// <summary>
        /// 所在区域
        /// </summary>
        public virtual string TypePath { get; set; }
    }
}
