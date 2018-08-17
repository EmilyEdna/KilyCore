using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.WEB.Model
{
    public class Contract
    {
        public string No { get => "YCKJ" + DateTime.Now.ToString("yyyyMMdd"); }
        public string CompanyName { get; set; }
        public string VersionName { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public int StarYear { get => DateTime.Now.Year; }
        public int StarMonth { get => DateTime.Now.Month; }
        public int StarDay { get => DateTime.Now.Day; }
        public int EndYear { get => DateTime.Now.Year + 1; }
        public int EndMonth { get => DateTime.Now.Month; }
        public int EndDay { get => DateTime.Now.Day; }
        public string CompanyTypeName { get; set; }
        public string Price { get; set; }
        public string Content { get; set; }
        public int Year { get => 1; }
    }
    public class ContractHelp
    {
        public string PathNo { get => DateTime.Now.ToString("yyyyMMddHHmm"); }
        public int StarYear { get => DateTime.Now.Year; }
        public int StarMonth { get => DateTime.Now.Month; }
        public int StarDay { get => DateTime.Now.Day; }
        public int EndYear { get => DateTime.Now.Year + ContractYear; }
        public int EndMonth { get => DateTime.Now.Month; }
        public int EndDay { get => DateTime.Now.Day; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string AttachInfo { get; set; }
        /// <summary>
        /// 合同类型 线上或线下
        /// </summary>
        public int ContractType { get; set; }
        /// <summary>
        /// 合同年限
        /// </summary>
        public int ContractYear { get; set; }
        /// <summary>
        /// 线下 授权公司
        /// </summary>
        public string AuthorCompany { get; set; }
        /// <summary>
        /// 线下 授权公司工商码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 线下 授权公司公章
        /// </summary>
        public string Chapter { get; set; }
        /// <summary>
        /// 线下 授权公司地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string VersionName { get; set; }
        /// <summary>
        /// 版本内容
        /// </summary>
        public string VersionDes { get; set; }
        /// <summary>
        /// 乙方公司
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 乙方代码
        /// </summary>
        public string CommunityCode { get; set; }
        /// <summary>
        /// 乙方地址
        /// </summary>
        public string CompanyAddress { get; set; }
    }
    public class FormData
    {
        public string Param { get; set; }
    }
    public static class Configer
    {
        public static string CompanySelf { get; set; }
        public static string CodeSelf { get; set; }
        public static string AddressSelf { get; set; }
        public static string Chapter { get; set; }
        public static string Host { get; set; }
    }
    public class ExcelModel
    {
        public string Ids { get; set; }
        [JsonIgnore]
        public string ApiUrl { get; set; }
        [JsonIgnore]
        public string TimeSpan { get => ((Int64)(new TimeSpan(DateTime.UtcNow.Ticks - (new DateTime(1970, 1, 1, 0, 0, 0).Ticks)).TotalMilliseconds)).ToString(); }
    }
}
