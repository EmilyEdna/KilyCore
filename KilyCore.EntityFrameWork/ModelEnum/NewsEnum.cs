using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：NewsEnum
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.ModelEnum
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/10/26 11:50:15
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 新闻分类
    /// </summary>
    public enum NewsEnum
    {
        /// <summary>
        /// 公示公告
        /// </summary>
        [Description("公示公告")]
        Notice = 10,
        /// <summary>
        /// 行业资讯
        /// </summary>
        [Description("行业资讯")]
        Infor = 20,
        /// <summary>
        /// 政策法规
        /// </summary>
        [Description("政策法规")]
        Rule =30,
        /// <summary>
        /// 健康生活
        /// </summary>
        [Description("健康生活")]
        Life =40,
        /// <summary>
        /// 曝光台
        /// </summary>
        [Description("曝光台")]
        Exposure =50
    }
}
