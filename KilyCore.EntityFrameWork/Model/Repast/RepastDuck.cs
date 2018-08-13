using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastDuck
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/13 11:46:42
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 废物处理
    /// </summary>
    public class RepastDuck: RepastBase
    {
        /// <summary>
        /// 处理方式
        /// </summary>
        public virtual string HandleWays { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public virtual string HandleUser { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public virtual DateTime? HandleTime { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 相关图片
        /// </summary>
        public virtual string HandleImg { get; set; }

    }
}
