using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Function
{
    public class RequestVeinTag
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 开始号段
        /// </summary>
        public Int64 StarSerialNo { get; set; }
        /// <summary>
        /// 结束号段
        /// </summary>
        public Int64 EndSerialNo { get; set; }
        /// <summary>
        /// 当前录入总个数
        /// </summary>
        public int TotalNo { get; set; }
        /// <summary>
        /// 使用个数
        /// </summary>
        public int UseNo { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string AcceptUser { get; set; }
        /// <summary>
        /// 接收人姓名获取公司名称
        /// </summary>
        public string AcceptUserName { get; set; }
        /// <summary>
        /// 接受时间
        /// </summary>
        public DateTime? AcceptTime { get; set; }
        /// <summary>
        /// 接受类型 1表示企业；2表示个人或运营商
        /// </summary>
        public int? AllotType { get; set; }
        /// <summary>
        /// 是否签收
        /// </summary>
        public bool IsAccept { get; set; }
    }
}
