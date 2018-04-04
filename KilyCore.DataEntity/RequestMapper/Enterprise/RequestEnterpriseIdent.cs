using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseIdent
    {
        #region 认证企业基本信息
        public Guid Id { get; set; }
        /// <summary>
        /// 认证编号
        /// </summary>
        public string IdentNo { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 企业类型名称
        /// </summary>
        public string CompanyTypeName { get; set; }
        /// <summary>
        /// 认证星级
        /// </summary>
        public string IdentStarName { get; set; }
        /// <summary>
        /// 认证年限
        /// </summary>
        public int IdentYear { get; set; }
        /// <summary>
        /// 信用代码
        /// </summary>
        public string CommunityCode { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string Representative { get; set; }
        /// <summary>
        /// 法人身份证
        /// </summary>
        public string RepresentativeCard { get; set; }
        /// <summary>
        /// 报送人
        /// </summary>
        public string SendPerson { get; set; }
        /// <summary>
        /// 报送人身份证
        /// </summary>
        public string SendCard { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public CompanyEnum? CompanyType { get; set; } 
        #endregion
        #region 辅助字段
        /// <summary>
        /// 区域树查询
        /// </summary>
        public string AreaTree { get; set; }
        #endregion
    }
}
