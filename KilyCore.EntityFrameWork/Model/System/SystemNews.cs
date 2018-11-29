using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：SystemNews
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.System
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/10/26 11:40:08
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 新闻表
    /// </summary>
    public class SystemNews: BaseEntity
    {
        /// <summary>
        /// 主标题
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public virtual string SubTitle { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public virtual DateTime? ReleaseDate { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public virtual string NewsContent { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public virtual string Writer { get; set; }
        /// <summary>
        /// 新闻分类
        /// </summary>
        public virtual NewsEnum NewsType { get; set; }
    }
}
