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
        /// 公司信息表主键
        /// </summary>
        public  Guid CompanyId { get; set; }
        /// <summary>
        /// 认证编号
        /// </summary>
        public  string IdentNo { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public  string CompanyName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public  CompanyEnum? CompanyType { get; set; }
        /// <summary>
        /// 认证开始时间
        /// </summary>
        public  DateTime IdentStartTime { get; set; }
        /// <summary>
        /// 认证截至时间
        /// </summary>
        public  DateTime IdentEndTime { get; set; }
        /// <summary>
        /// 认证星级
        /// </summary>
        public  IdentEnum IdentStar { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public  AuditEnum AuditType { get; set; }
        /// <summary>
        /// 认证年限
        /// </summary>
        public  int IdentYear { get; set; }
        /// <summary>
        /// 信用代码
        /// </summary>
        public  string CommunityCode { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public  string Representative { get; set; }
        /// <summary>
        /// 法人身份证
        /// </summary>
        public  string RepresentativeCard { get; set; }
        /// <summary>
        /// 报送人
        /// </summary>
        public  string SendPerson { get; set; }
        /// <summary>
        /// 报送人身份证
        /// </summary>
        public  string SendCard { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public  string LinkPhone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public  string Remark { get; set; }
        #endregion
        #region 辅助字段
        /// <summary>
        /// 区域树查询
        /// </summary>
        public string AreaTree { get; set; }
        #endregion
        #region 证书
        /// <summary>
        /// 企业认证表主键
        /// </summary>
        public Guid IdentId { get; set; }
        /// <summary>
        /// 法人身份证
        /// </summary>
        public  string ImgCard { get; set; }
        /// <summary>
        /// 申请表
        /// </summary>
        public  string ImgApply { get; set; }
        /// <summary>
        /// 调查表
        /// </summary>
        public  string ImgResearch { get; set; }
        /// <summary>
        /// 认证协议
        /// </summary>
        public  string ImgAgreement { get; set; }
        /// <summary>
        /// 合格证
        /// </summary>
        public  string ImgQualified { get; set; }
        /// <summary>
        /// 水质检测
        /// </summary>
        public  string ImgWater { get; set; }
        /// <summary>
        /// 土壤检测
        /// </summary>
        public  string ImgSoil { get; set; }
        /// <summary>
        /// 金属检测
        /// </summary>
        public  string ImgMetal { get; set; }
        /// <summary>
        /// 药品GSP检测
        /// </summary>
        public  string ImgDrugs { get; set; }
        /// <summary>
        /// 流通检测
        /// </summary>
        public  string ImgCirculation { get; set; }
        /// <summary>
        /// 卫生检测
        /// </summary>
        public  string ImgHygiene { get; set; }
        /// <summary>
        /// 进出口许可证
        /// </summary>
        public  string ImgImportExport { get; set; }
        /// <summary>
        /// 生产检测
        /// </summary>
        public  string ImgProduction { get; set; }
        /// <summary>
        /// 其他认证
        /// </summary>
        public  string ImgOther { get; set; }
        #endregion
    }
}
