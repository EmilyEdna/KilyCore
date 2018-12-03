using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.Function
{
    public class ResponseAreaDictionary
    {
        public Guid Id { get; set; }
        public string DicName { get; set; }
        public string DicValue { get; set; }
        public string DicDescript { get; set; }
        public string AttachInfo { get; set; }
        public bool? IsEnable { get; set; }
        public string States { get => (bool)IsEnable ? "禁用中" : "启用中"; }
        public IDictionary<String, String> ProvinceKeyValue { get; set; }
        /// <summary>
        /// 禁用区域
        /// </summary>
        public string DisArea { get; set; }
        /// <summary>
        /// 分配区域
        /// </summary>
        public string AttachArea { get; set; }
        public string DisableArea
        {
            get
            {
                if (string.IsNullOrEmpty(DisArea))
                    return null;
                else
                {
                    List<string> ls = new List<string>();
                    var strs = DisArea.Split("*").ToList();
                    foreach (var str in strs)
                    {
                        if (ProvinceKeyValue[str] != null)
                            ls.Add(ProvinceKeyValue[str]);
                    }
                    return string.Join("*", ls);
                }
            }
        }
    }
    public class ResponseAreaDic
    {
        public Guid Id { get; set; }
        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public Guid DictionaryId { get; set; }
    }
}
