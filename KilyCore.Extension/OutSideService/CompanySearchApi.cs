using KilyCore.Extension.HttpClientFactory;
using KilyCore.Extension.RSACryption;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KilyCore.Extension.OutSideService
{
    public class CompanySearchApi
    {
        public const string APPKEY = "257d4654ce6f423f9b373e6d32905e02";
        public const string SECERTKEY = "5569BA9C0EF39AA08BFB6A40D1D51704";
        public static CompanySearchApi GetOutSideApiSearchApi => new CompanySearchApi();
        /// <summary>
        /// 调用企查查外部接口
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        public RootData GetOutSideApi_SearchCompanyDetail(string CompanyName)
        {
            String KeyUrl = $"http://api.qichacha.com/ECIV4/GetDetailsByName?key={APPKEY}&keyword={CompanyName}";
            return JsonConvert.DeserializeObject<RootData>(HttpClientExtension.HttpGetAsync(KeyUrl, GetHeader()).Result);
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public string GetTimespan()
        {
            return ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString(); // 当地时区
        }
        /// <summary>
        /// 获取头部
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> GetHeader()
        {
            Dictionary<String, String> header = new Dictionary<String, String>();
            var Token = MD5CryptionExtension.MD5HASH(APPKEY + GetTimespan() + SECERTKEY);
            header.Add("Token", Token);
            header.Add("Timespan", GetTimespan());
            return header;
        }
        /// <summary>
        /// 校验企业是否真是存在
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="CompanyCode"></param>
        /// <returns></returns>
        public bool CheckCompany(String CompanyName, String CompanyCode)
        {
            var root = GetOutSideApi_SearchCompanyDetail(CompanyName);
            if (root.Status != 200)
                return false;
            else
                return (root.Result.Name.Equals(CompanyName) && root.Result.CreditCode.Equals(CompanyCode)) ? true : false;
        }
    }
    public class RootData
    {
        public ReulstData Result { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }
    public class ReulstData
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 监管政府
        /// </summary>
        public string BelongOrg { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string OperName { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 社会征信码
        /// </summary>
        public string CreditCode { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public string EconKind { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string Scope { get; set; }
    }
}
