using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseRepastSupplier
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/2 15:40:15
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastSupplier
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 经营类型1表示企业，2表示个人
        /// </summary>
        public int SupplierType { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string SupplierNo { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string SupplierUser { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 卫生许可证
        /// </summary>
        public string HealthCard { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string RunCard { get; set; }
    }
}
