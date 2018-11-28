using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseGovtComplain
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/28 11:51:36
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Govt
{
    public class ResponseGovtComplain
    {
        public Guid Id { get; set; }
        public Guid? GovtId { get; set; }
        /// <summary>
        /// 投诉人
        /// </summary>
        public string ComplainUser { get; set; }
        /// <summary>
        /// 投诉人电话
        /// </summary>
        public string ComplainUserPhone { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public Guid? CompanyId { get; set; }
        /// <summary>
        ///投诉公司
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public string CompanyType { get; set; }
        /// <summary>
        /// 投诉产品
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 投诉时间
        /// </summary>
        public DateTime? ComplainTime { get; set; }
        /// <summary>
        /// 投诉内容
        /// </summary>
        public string ComplainContent { get; set; }
        /// <summary>
        /// 处理内容
        /// </summary>
        public string HandlerContent { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}
