using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.System
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/15 14:23:15
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseSystemCodeCount
    {
        public string AreaName { get; set; }
        public int HistoryVeinCount { get; set; }
        public int NowVeinCount { get; set; }
        public int VienSum => HistoryVeinCount + NowVeinCount;
        public int HistoryThingCount { get; set; }
        public int NowThingCount { get; set; }
        public int ThingSum => HistoryThingCount + NowThingCount;
        public int HistoryClassCount { get; set; }
        public int NowClassCount { get; set; }
        public int ClassSum => HistoryClassCount + NowClassCount;
        public int HistoryCompanyCount { get; set; }
        public int NowCompanyCount { get; set; }
        public int CompanySum => HistoryCompanyCount + HistoryCompanyCount;
        public int MonthSum => NowVeinCount + NowThingCount + NowClassCount + NowCompanyCount;
        public int Total => NowVeinCount + NowThingCount + NowClassCount + NowCompanyCount
            + HistoryVeinCount + HistoryThingCount + HistoryClassCount + HistoryCompanyCount;
    }
}
