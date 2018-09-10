using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtBase
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Base
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/6 15:03:51
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Base
{
    /// <summary>
    /// 监管公用字段
    /// </summary>
    public class GovtBase : BaseEntity
    {
        /// <summary>
        /// 政府父Id
        /// </summary>
        public virtual Guid? GovtId { get; set; }
    }
}
