using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Function
{
    /// <summary>
    /// 纹理二维码表
    /// </summary>
    public class FunctionVeinTag : BaseEntity
    {
        /// <summary>
        /// 开始号段
        /// </summary>
        public virtual int StarSerialNo { get; set; }
        /// <summary>
        /// 结束号段
        /// </summary>
        public virtual int EndSerialNo { get; set; }
        /// <summary>
        /// 当前录入总个数
        /// </summary>
        public virtual int TotalNo { get; set; }
        /// <summary>
        /// 使用个数
        /// </summary>
        public virtual int UseNo { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public virtual string AcceptUser { get; set; }
        /// <summary>
        /// 接收人姓名获取公司名称
        /// </summary>
        public virtual string AcceptUserName { get; set; }
        /// <summary>
        /// 接受时间
        /// </summary>
        public virtual DateTime? AcceptTime{ get; set; }
        /// <summary>
        /// 分配类型 1表示企业；2表示运营商
        /// </summary> 
        public virtual int? AllotType { get; set; }
        /// <summary>
        /// 是否签收
        /// </summary>
        public virtual bool IsAccept { get; set; }
    }
}
