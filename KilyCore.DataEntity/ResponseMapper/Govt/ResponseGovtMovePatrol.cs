using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseGovtMovePatrol
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/25 17:01:16
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Govt
{
    public class ResponseGovtMovePatrol
    {
        public Guid Id { get; set; }
        public Guid? GovtId { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public Guid? CompanyId { get; set; }
        /// <summary>
        /// 执法类目表Id
        /// </summary>
        public Guid? CategoryId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public string CompanyType { get; set; }
        /// <summary>
        /// 类目名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 执法人
        /// </summary>
        public string PatrolUser { get; set; }
        /// <summary>
        /// 执法时间
        /// </summary>
        public DateTime? PatrolTime { get; set; }
        /// <summary>
        /// 执法表
        /// </summary>
        public string PatrolTable { get; set; }
        public string Sound { get; set; }
    }
}
