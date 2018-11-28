using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtTrainNotice
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/27 11:35:23
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 培训通知表
    /// </summary>
    public class GovtTrainNotice: GovtBase
    {
        /// <summary>
        /// 培训标题
        /// </summary>
        public virtual string TrainTitle { get; set; }
        /// <summary>
        /// 培训地点
        /// </summary>
        public virtual string TrainPlace { get; set; }
        /// <summary>
        /// 培训实际
        /// </summary>
        public virtual DateTime? TrainTime { get; set; }
        /// <summary>
        /// 培训内容
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public virtual string CompanyType { get; set; }
    }
    /// <summary>
    /// 培训报道表
    /// </summary>
    public class GovtTrainReport: GovtBase
    {
        /// <summary>
        /// 报道标题
        /// </summary>
        public virtual string InfoTitle { get; set; }
        /// <summary>
        /// 报道内容
        /// </summary>
        public virtual string InfoContent { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public virtual string CompanyType { get; set; }
    }
}
