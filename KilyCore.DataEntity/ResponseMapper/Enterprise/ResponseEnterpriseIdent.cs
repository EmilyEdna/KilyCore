using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseIdent
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
        /// 审核类型
        /// </summary>
        public AuditEnum AuditType { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public CompanyEnum CompanyType { get; set; }
        #endregion
        #region 所有企业公用字段
        /// <summary>
        /// 法人身份证
        /// </summary>
        public string ImgCard { get; set; }
        /// <summary>
        /// 申请表
        /// </summary>
        public string ImgApply { get; set; }
        /// <summary>
        /// 调查表
        /// </summary>
        public string ImgResearch { get; set; }
        /// <summary>
        /// 认证协议
        /// </summary>
        public string ImgAgreement { get; set; }
        /// <summary>
        /// 其他检测
        /// </summary>
        public string ImgOther { get; set; }
        #endregion
        #region 种养(植/殖)企业字段
        /// <summary>
        /// 合格证
        /// </summary>
        public string ImgQualified_X { get; set; }
        /// <summary>
        /// 水质检测
        /// </summary>
        public string ImgWater_X { get; set; }
        /// <summary>
        /// 土壤检测
        /// </summary>
        public string ImgSoil_X { get; set; }
        /// <summary>
        /// 金属检测
        /// </summary>
        public string ImgMetal_X { get; set; }
        #endregion
        #region 生产企业字段
        /// <summary>
        /// 生产检测
        /// </summary>
        public string ImgProduction_Y { get; set; }
        #endregion
        #region 餐饮企业字段
        /// <summary>
        /// 原料购买证明
        /// </summary>
        public string ImgMaterialOrder_F { get; set; }
        /// <summary>
        /// 消毒证明
        /// </summary>
        public string ImgDisinfection_F { get; set; }
        /// <summary>
        /// 原料储藏证明
        /// </summary>
        public string ImgMaterialSave_F { get; set; }
        /// <summary>
        /// 废弃物处理证明
        /// </summary>
        public string ImgAbandoned_F { get; set; }
        /// <summary>
        /// 留样证明
        /// </summary>
        public string ImgSample_F { get; set; }
        /// <summary>
        /// 从业证明
        /// </summary>
        public string ImgWorkingPerson_F { get; set; }
        #endregion
        #region 流通企业字段
        /// <summary>
        /// 流通检测
        /// </summary>
        public string ImgCirculation_M { get; set; }
        /// <summary>
        /// 进出口许可证
        /// </summary>
        public string ImgImportExport_M { get; set; }
        #endregion
        #region  流通企业和生产企业通用字段
        /// <summary>
        /// 药品GMP(GSP)检测
        /// </summary>
        public string ImgDrugs_Y_M { get; set; }
        /// <summary>
        /// 卫生检测
        /// </summary>
        public string ImgHygiene_Y_M { get; set; }
        #endregion
        #region 辅助字段
        /// <summary>
        /// 审核类型名称
        /// </summary>
        public string AuditTypeName { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        #endregion
        public IList<ResponseAudit> AuditInfo { get; set; }
    }
}
