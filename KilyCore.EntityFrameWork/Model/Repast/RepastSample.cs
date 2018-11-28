using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastSample
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/13 11:22:41
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 留样表
    /// </summary>
    public class RepastSample: RepastBase
    {
        /// <summary>
        /// 菜品名称
        /// </summary>
        public virtual string DishName { get; set; }
        /// <summary>
        /// 留样时间
        /// </summary>
        public virtual DateTime? SampleTime { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public virtual string OperatUser { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 留样图片
        /// </summary>
        public virtual string SampleImg { get; set; }
    }
}
