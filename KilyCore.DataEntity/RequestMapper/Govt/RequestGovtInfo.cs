using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestGovtInfo
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/6 15:52:44
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Govt
{
    public class RequestGovtInfo
    {
        public Guid Id { get; set; }
        public bool? IsEdu { get; set; }
        public Guid? GovtId { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 机构表Id
        /// </summary>
        public Guid? DepartId { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public string TypePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Province) || !string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(Area) || !string.IsNullOrEmpty(Town))
                    return Province + "," + City + "," + Area + "," + Town;
                else return null;
            }
        }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Town { get; set; }
        /// <summary>
        /// 账号类型
        /// </summary>
        public GovtAccountEnum AccountType { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }
        public bool IsApp { get; set; }
    }
}
