using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseSystemContractCount
    {
        /// <summary>
        /// 所在区域
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 种植数量
        /// </summary>
        public int Plant { get; set; }
        /// <summary>
        /// 养殖数量
        /// </summary>
        public int Feed { get; set; }
        /// <summary>
        /// 生产数量
        /// </summary>
        public int Production { get; set; }
        /// <summary>
        /// 流通数量
        /// </summary>
        public int Roop { get; set; }
        /// <summary>
        /// 餐饮数量
        /// </summary>
        public int Mer { get; set; }
        /// <summary>
        /// 单位数量
        /// </summary>
        public int Unit { get; set; }
        public int Alipay { get; set; }
        public int WxPay { get; set; }
        public int UnionPay { get; set; }
        public int AgentPay { get; set; }
        public int Test { get; set; }
        public int Base { get; set; }
        public int Lv { get; set; }
        public int LastVer { get; set; }
        public long ContractMoneny { get; set; }
        public long WayTags { get; set; }
        public long TargetMoneny { get; set; }
        public long LostMoneny { get; set; }
        public int CountYear { get; set; }

    }
    public class ResponseSystemContractTotalCount
    {
        public List<ResponseSystemContractCount> ContractCounts { get; set; }
        public int Psum { get; set; }
        public int Fsum { get; set; }
        public int Prsum { get; set; }
        public int Rsum { get; set; }
        public int Msum { get; set; }
        public int Usum { get; set; }
        public int AlipaySum { get; set; }
        public int WxPaySum { get; set; }
        public int UnionPaySum { get; set; }
        public int AgentPaySum { get; set; }
        public int TestSum { get; set; }
        public int BaseSum { get; set; }
        public int LvSum { get; set; }
        public int LastVerSum { get; set; }
        public long ContractMonenySum { get; set; }
        public long WayTagsSum { get; set; }
        public long TargetMonenySum { get; set; }
        public long LostMonenySum { get; set; }
        public int CountYearSum { get; set; }
    }
}
