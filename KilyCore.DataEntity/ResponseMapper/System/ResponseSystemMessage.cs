using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseSystemMessage
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.System
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/20 17:23:08
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseSystemMessage
    {
        /// <summary>
        /// 企业的Id
        /// </summary>
        public Guid? CompanyId { get; set; }
        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }
        /// <summary>
        /// 消息名称
        /// </summary>
        public string MsgName { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MsgContent { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public string TypePath { get; set; }
    }
}
