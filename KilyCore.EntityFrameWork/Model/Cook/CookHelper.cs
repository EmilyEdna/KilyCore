using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookHelper
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/29 10:33:05
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Cook
{
    /// <summary>
    /// 帮厨表
    /// </summary>
    public class CookHelper: CookBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string HelperName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 健康证
        /// </summary>
        public virtual string HealthCard { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual string TypePath { get; set; }
    }
}
