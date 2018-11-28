using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookBase
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Base
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 11:28:30
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Base
{
    /// <summary>
    /// 厨师公共字段表
    /// </summary>
    public class CookBase:BaseEntity
    {
        /// <summary>
        /// 厨师会员表Id
        /// </summary>
        public virtual Guid? CookId { get; set; }
    }
}
