using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestGovtMovePatrol
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/25 16:27:42
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Govt
{
    public class RequestGovtMovePatrol
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

        #region 模板表(执法表)字段
        public string CompanyAddress { get; set; }
        public string PersonName { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyCode { get; set; }
        public string CompanySign { get; set; }
        public string AnswerList { get; set; }
        public string ImgList { get; set; }
        public string Sound { get; set; }
        public string CheckRemark { get; set; }
        #endregion
    }
}
