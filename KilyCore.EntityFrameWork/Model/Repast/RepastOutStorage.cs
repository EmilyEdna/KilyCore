﻿using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastOutStorage
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/7 11:28:11
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    public class RepastOutStorage : RepastBase
    {
        /// <summary>
        /// 入库的Id
        /// </summary>
        public virtual Guid InStorageId { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 食材名称
        /// </summary>
        public virtual string IngredientName { get; set; }
        /// <summary>
        /// 出库数量
        /// </summary>
        public virtual int OutStorageNum { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        public virtual DateTime? OutStorageTime { get; set; }
        /// <summary>
        /// 出库负责人
        /// </summary>
        public virtual string OutUser { get; set; }
    }
}
