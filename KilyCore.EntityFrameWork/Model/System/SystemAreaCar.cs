using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.System
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/23 17:11:06
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 区域车牌
    /// </summary>
    public class SystemAreaCar: BaseEntity
    {
        /// <summary>
        /// 车牌
        /// </summary>
        public virtual string CarCard { get; set; }
        /// <summary>
        /// 区域代码
        /// </summary>
        public virtual string CityCode { get; set; }
    }
}
