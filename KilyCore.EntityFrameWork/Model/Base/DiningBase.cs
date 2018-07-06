using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：DiningBase
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Base
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/6 10:05:57
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Base
{
    /// <summary>
    /// 餐饮系统公共字段表
    /// </summary>
    public class DiningBase:BaseEntity
    {
        /// <summary>
        /// 餐饮用户主键
        /// </summary>
        public virtual Guid InfoId { get; set; }
    }
}
