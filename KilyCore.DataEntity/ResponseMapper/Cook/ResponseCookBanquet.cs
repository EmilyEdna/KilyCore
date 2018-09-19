using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseCookBanquet
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/29 11:29:49
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Cook
{
    public class ResponseCookBanquet
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 厨师会员表Id
        /// </summary>
        public Guid? CookId { get; set; }
        /// <summary>
        /// 举办者姓名
        /// </summary>
        public string HoldName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string TypePath { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 办宴天数
        /// </summary>
        public string HoldDay { get; set; }
        /// <summary>
        /// 办宴时间
        /// </summary>
        public DateTime? HoldTime { get; set; }
        /// <summary>
        /// 办宴类型
        /// </summary>
        public string HoldType { get; set; }
        /// <summary>
        /// 帮厨师
        /// </summary>
        public string Helper { get; set; }
        /// <summary>
        /// 水源
        /// </summary>
        public string Water { get; set; }
        /// <summary>
        /// 餐具消毒方式
        /// </summary>
        public string Disinfection { get; set; }
        /// <summary>
        /// 留样设施
        /// </summary>
        public string Facility { get; set; }
        /// <summary>
        /// 有毒有害物品
        /// </summary>
        public string Poisonous { get; set; }
        /// <summary>
        /// 加工卫生
        /// </summary>
        public string Processing { get; set; }
        /// <summary>
        /// 食材
        /// </summary>
        public string Ingredients { get; set; }
        /// <summary>
        /// 菜谱
        /// </summary>
        public string CookBook { get; set; }
        /// <summary>
        /// 待批复，待检查，完成
        /// </summary>
        public string Stauts { get; set; }
        /// <summary>
        /// 检查结果-图片
        /// </summary>
        public string ResultImg { get; set; }
        /// <summary>
        /// 报备时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public string ExpiredTime => HoldTime.HasValue ? HoldTime.Value.AddDays(Convert.ToInt32(HoldDay)).ToString() : null;
        public int UserCount => !string.IsNullOrEmpty(Helper) ? Helper.Split(",").Length : 0;
        public string Province => !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 1 ? TypePath.Split(',')[0] : null) : null;
        public string City => !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 2 ? TypePath.Split(',')[1] : null) : null;
        public string Area => !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 3 ? TypePath.Split(',')[2] : null) : null;
        public string Town => !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 4 ? (TypePath.Split(',')[3]) : null) : null;
        public IList<ResponseCookHelper> Helpers { get; set; }
    }
}
