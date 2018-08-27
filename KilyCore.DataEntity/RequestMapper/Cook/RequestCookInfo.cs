using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestCookInfo
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 14:32:52
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Cook
{
    public class RequestCookInfo
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 厨师会员表Id
        /// </summary>
        public Guid? CookId { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sexlab { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 发证机关
        /// </summary>
        public string CardOffice { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiredDay { get; set; }
        /// <summary>
        /// 身份证正反面
        /// </summary>
        public string IdCardPhoto { get; set; }
        /// <summary>
        /// 登记证
        /// </summary>
        public string BookInCard { get; set; }
        /// <summary>
        /// 培训合格证
        /// </summary>
        public string TrainCard { get; set; }
        /// <summary>
        /// 是否会员
        /// </summary>
        public bool IsVip { get; set; }
        /// <summary>
        /// 开通时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public  Guid? RoleId { get; set; }
        /// <summary>
        /// 所在区域
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
    }
}
