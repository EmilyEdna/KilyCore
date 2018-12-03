using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Function
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/3 9:47:06
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.EntityFrameWork.Model.Function
{
    public class FunctionDisDictionary: BaseEntity
    {
        /// <summary>
        /// 区域码表Id
        /// </summary>
        public virtual Guid? AreaDicId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool? IsEnable { get; set; }
        /// <summary>
        /// 省份Id
        /// </summary>
        public virtual string ProvinceId { get; set; }
    }
}
