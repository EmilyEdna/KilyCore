using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtInstitution
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/11 14:19:56
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 政府机构表
    /// </summary>
    public class GovtInstitution: GovtBase
    {
        /// <summary>
        /// 机构名称
        /// </summary>
        public virtual string InstitutionName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string ChargeUser { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 管理区域
        /// </summary>
        public virtual string ManageArea { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
