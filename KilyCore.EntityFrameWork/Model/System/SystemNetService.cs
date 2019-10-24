using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 服务网点
    /// </summary>
    public class SystemNetService: BaseEntity
    {

        /// <summary>
        /// 网点名称
        /// </summary>
        public virtual string ServiceNetName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 信誉代码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public virtual string Off { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 营业执照副本
        /// </summary>
        public virtual string IdImage { get; set; }
        /// <summary>
        /// 服务年限
        /// </summary>
        public virtual string ServiceYear { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? STime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? ETime { get; set; }
        /// <summary>
        /// 服务区域
        /// </summary>
        public virtual string ServciePath { get; set; }
    }
}
