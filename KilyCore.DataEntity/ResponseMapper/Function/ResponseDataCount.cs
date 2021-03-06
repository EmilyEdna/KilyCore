﻿using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseDataCount
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Function
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/22 14:46:12
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Function
{
    public class ResponseDataCount
    {
        /// <summary>
        /// 表示统计图类型 true表示饼状 false表示柱状
        /// </summary>
        public bool Type { get; set; }
        public string Name { get; set; }
        public IList<String> DataTitle { get; set; }
        public IList<DataPie> InSideData { get; set; }
        public IList<DataPie> OutSideData { get; set; }
        public IList<DataBar> BarData { get; set; }
    }
    /// <summary>
    /// 饼状数据
    /// </summary>
    public class DataPie
    {
        public int value { get; set; }
        public string name { get; set; }

        public string url { set; get; }
    }
    /// <summary>
    /// 柱状数据
    /// </summary>
    public class DataBar
    {
        public string name { get; set; }
        public string type { get => "bar"; }
        public IList<int> data { get; set; }
    }
    /// <summary>
    /// 折线图
    /// </summary>
    public class DataLine
    {
        public string name { get; set; }
        public string type { get => "line"; }
        public IList<int> data { get; set; }
    }
}
