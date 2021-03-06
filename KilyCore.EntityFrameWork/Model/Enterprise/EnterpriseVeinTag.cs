﻿using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseVeinTag
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/14 14:22:42
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 企业纹理二维码表
    /// </summary>
    public class EnterpriseVeinTag:EnterpriseBase
    {
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
        /// 使用数量
        /// </summary>
        public virtual int UseNum { get; set; }
        /// <summary>
        /// 接受编号
        /// </summary>
        public virtual string AcceptNo { get; set; }
    }
}
