using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtMovePatrol
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/25 16:04:16
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 移动执法表
    /// </summary>
    public class GovtMovePatrol: GovtBase
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public virtual Guid? CompanyId { get; set; }
        /// <summary>
        /// 执法类目表Id
        /// </summary>
        public virtual Guid? CategoryId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public virtual string CompanyType { get; set; }
        /// <summary>
        /// 类目名称
        /// </summary>
        public virtual string CategoryName { get; set; }
        /// <summary>
        /// 执法人
        /// </summary>
        public virtual string PatrolUser { get; set; }
        /// <summary>
        /// 执法时间
        /// </summary>
        public virtual DateTime? PatrolTime { get; set; }
        /// <summary>
        /// 执法表
        /// </summary>
        public virtual string PatrolTable { get; set; }
        /// <summary>
        /// 图片列表
        /// </summary>
        public virtual string ImgList { get; set; }
        /// <summary>
        /// 录音
        /// </summary>
        public virtual string Sound { get; set; }
    }
}
