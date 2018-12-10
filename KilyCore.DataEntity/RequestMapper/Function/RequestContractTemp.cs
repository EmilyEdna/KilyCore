using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/10 15:40:12
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Function
{
    public class RequestContractTemp
    {
        public long? TagNum { get; set; }
        public Guid MerchantId { get; set; }
        public Guid GoodsId { get; set; }
        public SystemVersionEnum VersionType { get; set; }
    }
}
