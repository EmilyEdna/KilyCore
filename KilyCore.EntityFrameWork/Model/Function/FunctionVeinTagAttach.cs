using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：FunctionVeinTagAttach
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Function
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/14 10:46:58
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Function
{
    /// <summary>
    /// 纹理二维码运营中心表
    /// </summary>
    public class FunctionVeinTagAttach:BaseEntity
    {
        /// <summary>
        /// 自身批次号
        /// </summary>
        public virtual string SingleBatchNo { get; set; }
        /// <summary>
        /// 父表批次号
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 开始号段
        /// </summary>
        public virtual Int64 StarSerialNo { get; set; }
        /// <summary>
        /// 结束号段
        /// </summary>
        public virtual Int64 EndSerialNo { get; set; }
        /// <summary>
        /// 当前录入总个数
        /// </summary>
        public virtual int TotalNo { get; set; }
        /// <summary>
        /// 接收人Id
        /// </summary>
        public virtual string AcceptUser { get; set; }
        /// <summary>
        /// 接收人姓名获取公司名称
        /// </summary>
        public virtual string AcceptUserName { get; set; }
        /// <summary>
        /// 接受时间
        /// </summary>
        public virtual DateTime? AcceptTime { get; set; }
        /// <summary>
        /// 分配类型 1表示企业；2表示运营商
        /// </summary> 
        public virtual int? AllotType { get; set; }
        /// <summary>
        /// 是否签收
        /// </summary>
        public virtual bool IsAccept { get; set; }
        /// <summary>
        /// 分配数量
        /// </summary>
        public virtual int AllotNum { get; set; }
    }
}
