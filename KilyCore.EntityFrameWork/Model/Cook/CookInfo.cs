using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookInfo
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 11:56:20
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Cook
{
    /// <summary>
    /// 厨师信息
    /// </summary>
    public class CookInfo : CookBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string TrueName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sexlab { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public virtual string Nation { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string IdCardNo { get; set; }
        /// <summary>
        /// 所在区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 发证机关
        /// </summary>
        public virtual string CardOffice { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public virtual DateTime? ExpiredDay { get; set; }
        /// <summary>
        /// 身份证正反面
        /// </summary>
        public virtual string IdCardPhoto { get; set; }
        /// <summary>
        /// 登记证
        /// </summary>
        public virtual string BookInCard { get; set; }
        /// <summary>
        /// 培训合格证
        /// </summary>
        public virtual string TrainCard { get; set; }
        /// <summary>
        /// 审核
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }
    }
}
