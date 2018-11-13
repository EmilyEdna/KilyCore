using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestSystemNews
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.System
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/10/26 11:45:38
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestSystemNews
    {
        public Guid Id { get; set; }
        public NewsEnum NewsType { get; set; }
        /// <summary>
        /// 主标题
        /// </summary>
        public  string Title { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public  DateTime? ReleaseDate { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public  string NewsContent { get; set; }
    }
}
