using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookBanquet
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/29 9:35:56
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Cook
{
    /// <summary>
    /// 群宴报备表
    /// </summary>
    public class CookBanquet : CookBase
    {
        /// <summary>
        /// 举办者姓名
        /// </summary>
        public virtual string HoldName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 办宴天数
        /// </summary>
        public virtual string HoldDay { get; set; }
        /// <summary>
        /// 办宴时间
        /// </summary>
        public virtual DateTime? HoldTime { get; set; }
        /// <summary>
        /// 办宴类型
        /// </summary>
        public virtual string HoldType { get; set; }
        /// <summary>
        /// 帮厨师
        /// </summary>
        public virtual string Helper { get; set; }
        /// <summary>
        /// 水源
        /// </summary>
        public virtual string Water { get; set; }
        /// <summary>
        /// 餐具消毒方式
        /// </summary>
        public virtual string Disinfection { get; set; }
        /// <summary>
        /// 留样设施
        /// </summary>
        public virtual string Facility { get; set; }
        /// <summary>
        /// 有毒有害物品
        /// </summary>
        public virtual string Poisonous { get; set; }
        /// <summary>
        /// 加工卫生
        /// </summary>
        public virtual string Processing { get; set; }
        /// <summary>
        /// 食材
        /// </summary>
        public virtual string Ingredients { get; set; }
        /// <summary>
        /// 菜谱
        /// </summary>
        public virtual string CookBook { get; set; }
        /// <summary>
        /// 待批复，待检查，完成
        /// </summary>
        public virtual string Stauts { get; set; }
        /// <summary>
        /// 检查结果-图片
        /// </summary>
        public virtual string ResultImg { get; set; }
        /// <summary>
        /// 宴会主题
        /// </summary>
        public virtual string HoldTheme { get; set; }
        /// <summary>
        /// 宴会桌数
        /// </summary>
        public virtual string DeskNum { get; set; }
        /// <summary>
        /// 宴会人数
        /// </summary>
        public virtual string HoldTotal { get; set; }
        /// <summary>
        /// 主要食品原料及来源
        /// </summary>
        public virtual string HoldFoo { get; set; }
    }
}
