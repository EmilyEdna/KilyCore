using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestRepastScanInfo
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/2 9:54:36
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Repast
{
    public class RequestRepastScanInfo
    {
        public Guid Id { get; set; }
        public Guid? InfoId { get; set; }
        /// <summary>
        /// 记录名称
        /// </summary>
        public string RecordName { get; set; }
        /// <summary>
        /// <summary>
        /// 菜品Ids
        /// </summary>
        public string DishIds { get; set; }
        /// <summary>
        /// 原料Ids
        /// </summary>
        public string StuffIds { get; set; }
        /// <summary>
        /// 视频Ids
        /// </summary>
        public string VideoIds { get; set; }
        /// <summary>
        /// 用户Ids
        /// </summary>
        public string UserIds { get; set; }
        /// <summary>
        /// 废物处理Ids
        /// </summary>
        public string DuckIds { get; set; }
        /// <summary>
        /// 抽样Ids
        /// </summary>
        public string DrawIds { get; set; }
        /// <summary>
        /// 消毒剂Ids
        /// </summary>
        public string DisinfectIds { get; set; }
        /// <summary>
        /// 留样Ids
        /// </summary>
        public string SampleIds { get; set; }
        /// <summary>
        /// 添加剂Ids
        /// </summary>
        public string AdditiveIds { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime? ShowTime { get; set; }
    }
}
