using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseRepastDictionary
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Repast
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/8/1 10:39:03
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastDictionary
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        public string DicType { get; set; }
        public string DicName { get; set; }
        public string DicValue { get; set; }
        public string Remark { get; set; }
        public IList<ResponseRepastDictionary> DictionaryList { get; set; }
    }
}
