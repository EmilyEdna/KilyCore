using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ConfigMoney
* 类 描 述 ：
* 命名空间 ：KilyCore.Configure
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/28 14:18:45
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Configure
{
    /// <summary>
    /// 版本价格
    /// </summary>
    public static class ConfigMoney
    {
        /// <summary>
        /// 三小企业
        /// </summary>
        public static int Common { get; set; }
        /// <summary>
        /// 乡村厨师
        /// </summary>
        public static int Cook { get; set; }
        #region 体验版
        public static int PlantAndCultureTest { get; set; }
        public static int ProductionTest { get; set; }
        public static int CirculationTest { get; set; }
        public static int UnitCanteenTest { get; set; }
        public static int RepastTest { get; set; }
        #endregion
        #region 基础版
        public static int PlantAndCultureBase { get; set; }
        public static int ProductionBase { get; set; }
        public static int CirculationBase { get; set; }
        public static int UnitCanteenBase { get; set; }
        public static int RepastBase { get; set; }
        #endregion
        #region 升级版
        public static int PlantAndCultureLv { get; set; }
        public static int ProductionLv { get; set; }
        public static int CirculationLv { get; set; }
        public static int UnitCanteenLv { get; set; }
        public static int RepastLv { get; set; }
        #endregion
        #region 旗舰版
        public static int PlantAndCultureEnterprise { get; set; }
        public static int ProductionEnterprise { get; set; }
        public static int CirculationEnterprise { get; set; }
        public static int UnitCanteenEnterprise { get; set; }
        public static int RepastEnterprise { get; set; }
        #endregion
    }
}
