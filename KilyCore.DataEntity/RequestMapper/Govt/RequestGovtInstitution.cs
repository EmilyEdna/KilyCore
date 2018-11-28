using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestGovtInstitution
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/11 14:38:14
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Govt
{
    public class RequestGovtInstitution
    {
        public Guid Id { get; set; }
        public Guid? GovtId { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public virtual string InstitutionName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string ChargeUser { get; set; }
        /// <summary>
        /// 管理区域
        /// </summary>
        public virtual string ManageArea { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 管理区域名称
        /// </summary>
        public string ManageAreaName { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public string TypePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Province) || !string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(Area))
                    return Province + "," + City + "," + Area;
                else return null;
            }
        }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
    }
}
