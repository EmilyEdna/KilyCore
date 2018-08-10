using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseRepastTypeName
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/10 16:31:25
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastTypeName
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        public string TypeNames { get; set; }
        public int Types { get; set; }
        public string Type { get => Types == 1 ? "原料" : "物品"; }
        public string Spec { get; set; }
    }
}
