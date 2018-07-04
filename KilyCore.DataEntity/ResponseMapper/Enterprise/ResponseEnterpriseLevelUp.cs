using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseLevelUp
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/3 10:19:45
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseLevelUp
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid Id { get; set; }
        public DateTime? StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Year { get; set; }
        public string VersionName { get; set; }
        /// <summary>
        /// 二维码数量
        /// </summary>
        public string CodeNumber
        {
            get
            {
                if (VersionType == SystemVersionEnum.Test)
                    return 10000.ToString();
                else if (VersionType == SystemVersionEnum.Base)
                    return 100000.ToString();
                else if (VersionType == SystemVersionEnum.Level)
                    return 1000000.ToString();
                else
                    return 50000000.ToString();
            }
        }
        public SystemVersionEnum VersionType { get; set; }
    }
    public class ResponseEnterpriseInsideFile
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 文件标题
        /// </summary>
        public string FileTitle { get; set; }
        /// <summary>
        /// 文件内容
        /// </summary>
        public string FileContent { get; set; }
    }
}
