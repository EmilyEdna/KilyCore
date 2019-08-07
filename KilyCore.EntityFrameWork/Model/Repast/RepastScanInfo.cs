using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastScanInfo
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/2 8:58:51
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 餐饮扫码表
    /// </summary>
    public class RepastScanInfo : RepastBase
    {
        /// <summary>
        /// 记录名称
        /// </summary>
        public virtual string RecordName { get; set; }
        /// <summary>
        /// 菜品Ids
        /// </summary>
        public virtual string DishIds { get; set; }
        /// <summary>
        /// 原料Ids
        /// </summary>
        public virtual string StuffIds { get; set; }
        /// <summary>
        /// 视频Ids
        /// </summary>
        public virtual string VideoIds { get; set; }
        /// <summary>
        /// 用户Ids
        /// </summary>
        public virtual string UserIds { get; set; }
        /// <summary>
        /// 废物处理Ids
        /// </summary>
        public virtual string DuckIds { get; set; }
        /// <summary>
        /// 抽样Ids
        /// </summary>
        public virtual string DrawIds { get; set; }
        /// <summary>
        /// 消毒剂Ids
        /// </summary>
        public virtual string DisinfectIds { get; set; }
        /// <summary>
        /// 留样Ids
        /// </summary>
        public virtual string SampleIds { get; set; }
        /// <summary>
        /// 添加剂Ids
        /// </summary>
        public virtual string AdditiveIds { get; set; }
        /// <summary>
        /// 台账Ids
        /// </summary>
        public virtual string Tickets { get; set; }
        /// <summary>
        /// 周菜谱Ids
        /// </summary>
        public virtual string WeekMenus { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        public virtual DateTime? ShowTime { get; set; }
    }
}
