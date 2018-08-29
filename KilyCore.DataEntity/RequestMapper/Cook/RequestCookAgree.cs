using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestCookAgree
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/29 11:14:16
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Cook
{
    public class RequestCookAgree
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 厨师会员表Id
        /// </summary>
        public Guid? CookId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string AgreeConent { get; set; }
    }
}
