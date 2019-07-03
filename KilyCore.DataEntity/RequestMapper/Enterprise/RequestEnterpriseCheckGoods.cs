using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseCheckGoods
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/19 15:33:05
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseCheckGoods
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 检测自定义名称
        /// </summary>
        public string CheckName { get; set; }
        /// <summary>
        /// 产品表Id
        /// </summary>
        public Guid? GoodsId { get; set; }
        /// <summary>
        /// 进货表Id
        /// </summary>
        public Guid? BuyerId { get; set; }
        /// <summary>
        /// 日记表Id
        /// </summary>
        public Guid? NoteId { get; set; }
        /// <summary>
        /// 质检单位
        /// </summary>
        public string CheckUint { get; set; }
        /// <summary>
        /// 质检人员
        /// </summary>
        public string CheckUser { get; set; }
        /// <summary>
        /// 质检结果
        /// </summary>
        public string CheckResult { get; set; }
        /// <summary>
        /// 质检报告
        /// </summary>
        public string CheckReport { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
    }
}
