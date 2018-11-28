using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseRepastAdditive
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/10/10 12:42:20
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastAdditive
    {
        public Guid Id { get; set; }
        public Guid? InfoId { get; set; }
        /// <summary>
        /// 添加剂名称
        /// </summary>
        public string AdditiveName { get; set; }
        /// <summary>
        /// 生产商
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime? UseTime { get; set; }
        /// <summary>
        /// 生产时间
        /// </summary>
        public DateTime? SupplierTime { get; set; }
        /// <summary>
        /// 使用计量
        /// </summary>
        public string Metering { get; set; }
        /// <summary>
        /// 使用人
        /// </summary>
        public string UsePerson { get; set; }
        /// <summary>
        /// 制作的食品
        /// </summary>
        public string ProFood { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string Function { get; set; }
    }
}
