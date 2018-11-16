using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.System
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/16 10:13:25
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseSystemProductCount
    {
        public string AreaName { get; set; }
        public int HistoryFarmer { get; set; }
        public int NowFarmer { get; set; }
        public int FarmerSum => HistoryFarmer + NowFarmer;
        public int HistoryFood { get; set; }
        public int NowFood { get; set; }
        public int FoodSum => HistoryFood + NowFood;
        public int HistoryDrug { get; set; }
        public int NowDrug { get; set; }
        public int DrugSum => HistoryDrug + NowDrug;
        public int HistoryCosplay { get; set; }
        public int NowCosplay { get; set; }
        public int CosplaySum => HistoryCosplay + NowCosplay;
        public int HistoryMachine { get; set; }
        public int NowMachine { get; set; }
        public int MachineSum => HistoryMachine + NowMachine;
        public int HistoryOhter { get; set; }
        public int NowOhter { get; set; }
        public int OhterSum => HistoryOhter + NowOhter;
        public int MonthSum => NowFarmer + NowFood + NowDrug + NowCosplay + NowMachine + NowOhter;
        public int Total => NowFarmer + NowFood + NowDrug + NowCosplay + NowMachine + NowOhter
            + HistoryFarmer + HistoryFood + HistoryDrug + HistoryCosplay + HistoryMachine + HistoryOhter;
    }
}
