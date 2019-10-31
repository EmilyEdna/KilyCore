using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastDish
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/6 11:53:20
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 菜品表
    /// </summary>
    public class RepastDish: RepastBase
    {
        /// <summary>
        /// 菜谱名称
        /// </summary>
        public virtual string DishName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public virtual string DishType { get; set; }
        /// <summary>
        /// 配料
        /// </summary>
        public virtual string Batching { get; set; }
        /// <summary>
        /// 主料
        /// </summary>
        public virtual string MainBatch { get; set; }
        /// <summary>
        /// 调料
        /// </summary>
        public virtual string Seasoning { get; set; }
        /// <summary>
        /// 口味
        /// </summary>
        public virtual string Taste { get; set; }
        /// <summary>
        /// 烹饪方式
        /// </summary>
        public virtual string CookingType { get; set; }
        /// <summary>
        /// 烹饪时长
        /// </summary>
        public virtual string CookingTime { get; set; }
        /// <summary>
        /// 菜品介绍
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string DishImg { get; set; }
    }
}
