using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.EntityMapping.Enterprise
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/21 11:55:52
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 视频监控
    /// </summary>
    public class EnterpriseVedio: EnterpriseBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string VedioName { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public virtual string VedioAddr { get; set; }
        /// <summary>
        /// 视频封面
        /// </summary>
        public virtual string VedioCover { get; set; }
        /// <summary>
        /// 是否首页显示
        /// </summary>
        public virtual bool IsIndex { get; set; }
        /// <summary>
        /// 所在区域
        /// </summary>
        public virtual string TypePath { get; set; }
    }
}
