using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
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
        public string BatchNo { get; set; }
        /// <summary>
        /// 自身批次号
        /// </summary>
        public string SingleBatchNo { get; set; }
    }
}
