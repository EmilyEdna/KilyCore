using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtAgree
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/10/10 13:06:36
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 责任协议
    /// </summary>
    public class GovtAgree: GovtBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public virtual string AgreeConent { get; set; }
    }
}
