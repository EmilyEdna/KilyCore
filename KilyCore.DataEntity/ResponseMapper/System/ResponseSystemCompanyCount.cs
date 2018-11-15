using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.System
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/15 17:00:44
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseSystemCompanyCount
    {
        public string AreaName { get; set; }
        public int HistoryPlant { get; set; }
        public int NowPlant { get; set; }
        public int PlantSum => HistoryPlant + NowPlant;
        public int HistoryCulture { get; set; }
        public int NowCulture { get; set; }
        public int CultureSum => HistoryCulture + NowCulture;
        public int HistoryProduction { get; set; }
        public int NowProduction { get; set; }
        public int ProductionSum => HistoryProduction + NowProduction;
        public int HistoryCirculation { get; set; }
        public int NowCirculation { get; set; }
        public int CirculationSum => HistoryCirculation + NowCirculation;
        public int HistoryOther { get; set; }
        public int NowOther { get; set; }
        public int OtherSum => HistoryOther + NowOther;
        public int HistoryNormal { get; set; }
        public int NowNormal { get; set; }
        public int NormalSum => HistoryNormal + NowNormal;
        public int HistoryUnitCanteen { get; set; }
        public int NowUnitCanteen { get; set; }
        public int UnitCanteenSum => HistoryUnitCanteen + NowUnitCanteen;
        public int HistorySmall { get; set; }
        public int NowSmall { get; set; }
        public int SmallSum => HistorySmall + NowSmall;
        public int HistoryCook { get; set; }
        public int NowCook { get; set; }
        public int CookSum => HistoryCook + NowCook;
        public int MonthSum => NowPlant + NowCulture + NowCirculation + NowProduction + NowOther + NowNormal + NowUnitCanteen + NowSmall + NowCook;
        public int Total => NowPlant + NowCulture + NowCirculation + NowProduction + NowOther + NowNormal + NowUnitCanteen + NowSmall + NowCook + HistorySmall
    + HistoryPlant + HistoryCulture + HistoryProduction + HistoryCirculation + HistoryOther + HistoryUnitCanteen + HistoryNormal + HistoryCook;
    }
}
