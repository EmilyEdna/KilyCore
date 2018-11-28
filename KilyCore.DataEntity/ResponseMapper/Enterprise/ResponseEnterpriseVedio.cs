using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/21 14:01:12
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseVedio
    {
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string VedioName { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public string VedioAddr { get; set; }
        /// <summary>
        /// 视频地址3
        /// </summary>
        public string VedioCover { get; set; }
    }
}
