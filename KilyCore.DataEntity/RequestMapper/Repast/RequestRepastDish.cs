using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestRepastDish
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/6 15:15:55
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Repast
{
    public class RequestRepastDish
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 菜谱名称
        /// </summary>
        public  string DishName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public  string DishType { get; set; }
        /// <summary>
        /// 配料
        /// </summary>
        public  string Batching { get; set; }
        /// <summary>
        /// 主料
        /// </summary>
        public  string MainBatch { get; set; }
        /// <summary>
        /// 调料
        /// </summary>
        public  string Seasoning { get; set; }
        /// <summary>
        /// 口味
        /// </summary>
        public  string Taste { get; set; }
        /// <summary>
        /// 烹饪方式
        /// </summary>
        public  string CookingType { get; set; }
        /// <summary>
        /// 烹饪时长
        /// </summary>
        public  string CookingTime { get; set; }
        /// <summary>
        /// 菜品介绍
        /// </summary>
        public  string Remark { get; set; }
    }
}
