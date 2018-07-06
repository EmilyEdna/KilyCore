using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastIdent
    {
        /// <summary>
        /// 认证编号
        /// </summary>
        public string IdentNo { get; set; }
        /// <summary>
        /// 认证开始时间
        /// </summary>
        public  DateTime IdentStartTime { get; set; }
        /// <summary>
        /// 认证截至时间
        /// </summary>
        public  DateTime IdentEndTime { get; set; }
        public string TableName { get; set; }
        public Guid Id { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public  string MerchantName { get; set; }
        /// <summary>
        /// 认证星级
        /// </summary>
        public  IdentEnum IdentStar { get; set; }
        public string IdentStarName { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public  AuditEnum AuditType { get; set; }
        public string AuditTypeName { get; set; }
        /// <summary>
        /// 商家类型
        /// </summary>
        public  MerchantEnum DiningType { get; set; }
        public string DiningTypeName { get; set; }
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

        #region 餐饮企业字段
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
        /// 原料购买证明
        /// </summary>
        public  string ImgMaterialOrder { get; set; }
        /// <summary>
        /// 消毒证明
        /// </summary>
        public  string ImgDisinfection { get; set; }
        /// <summary>
        /// 原料储藏证明
        /// </summary>
        public  string ImgMaterialSave { get; set; }
        /// <summary>
        /// 废弃物处理证明
        /// </summary>
        public  string ImgAbandoned { get; set; }
        /// <summary>
        /// 留样证明
        /// </summary>
        public  string ImgSample { get; set; }
        /// <summary>
        /// 从业证明
        /// </summary>
        public  string ImgWorkingPerson { get; set; }
        /// <summary>
        /// 其他证明
        /// </summary>
        public  string ImgOther { get; set; }
        #endregion
        /// <summary>
        /// 审核信息
        /// </summary>
        public IList<ResponseAudit> AuditInfo { get; set; }
    }
}
