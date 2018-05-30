using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月30日15点42分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseAgeUp
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string BacthNo { get; set; }
        /// <summary>
        /// 阶段名称
        /// </summary>
        public string LvName { get; set; }
        /// <summary>
        /// 阶段图片
        /// </summary>
        public string LvImg { get; set; }
    }
}
