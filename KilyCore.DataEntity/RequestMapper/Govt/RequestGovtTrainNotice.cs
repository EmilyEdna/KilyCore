using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestGovtTrainNotice
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/27 14:43:43
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Govt
{
    public class RequestGovtTrainNotice
    {
        public Guid Id { get; set; }
        public Guid? GovtId { get; set; }
        public string TrainTitle { get; set; }
        public string TrainPlace { get; set; }
        public DateTime? TrainTime { get; set; }
        public string Remark { get; set; }
        public string CompanyType { get; set; }
    }
    public class RequestGovtTrainReport
    {
        public Guid Id { get; set; }
        public Guid? GovtId { get; set; }
        public string InfoTitle { get; set; }
        public string InfoContent { get; set; }
        public string CompanyType { get; set; }
    }
}
