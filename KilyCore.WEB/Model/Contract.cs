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
        public String No { get => "YCKJ" + DateTime.Now.ToString("yyyyMMdd"); }
        public String CompanyName { get; set; }
        public String VersionName { get; set; }
        public String Address { get; set; }
        public String Code { get; set; }
        public int StarYear { get => DateTime.Now.Year; }
        public int StarMonth { get => DateTime.Now.Month; }
        public int StarDay { get => DateTime.Now.Day; }
        public int EndYear { get => DateTime.Now.Year + 1; }
        public int EndMonth { get => DateTime.Now.Month; }
        public int EndDay { get => DateTime.Now.Day; }
        public String CompanyTypeName { get; set; }
        public String Price { get; set; }
        public String Content { get; set; }
        public int Year { get => 1; }
    }
    public class ContractHelp
    {
        public String PathNo { get => DateTime.Now.ToString("yyyyMMddHHmm"); }
        public int StarYear { get => DateTime.Now.Year; }
        public int StarMonth { get => DateTime.Now.Month; }
        public int StarDay { get => DateTime.Now.Day; }
        public int EndYear { get => DateTime.Now.Year + ContractYear; }
        public int EndMonth { get => DateTime.Now.Month; }
        public int EndDay { get => DateTime.Now.Day; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public String AttachInfo { get; set; }
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
        public String AuthorCompany { get; set; }
        /// <summary>
        /// 线下 授权公司工商码
        /// </summary>
        public String Code { get; set; }
        /// <summary>
        /// 线下 授权公司公章
        /// </summary>
        public String Chapter { get; set; }
        /// <summary>
        /// 线下 授权公司地址
        /// </summary>
        public String Address { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public String VersionName { get; set; }
        /// <summary>
        /// 版本内容
        /// </summary>
        public String VersionDes { get; set; }
        /// <summary>
        /// 乙方公司
        /// </summary>
        public String CompanyName { get; set; }
        /// <summary>
        /// 乙方代码
        /// </summary>
        public String CommunityCode { get; set; }
        /// <summary>
        /// 乙方地址
        /// </summary>
        public String CompanyAddress { get; set; }
    }
    public class FromData
    {
        public String Path { get; set; }
    }
    public static class Configer
    {
        public static String CompanySelf { get; set; }
        public static String CodeSelf { get; set; }
        public static String AddressSelf { get; set; }
        public static String Chapter { get; set; }
        public static String Host { get; set; }
        public static String WebHost { get; set; }
    }
    public class ExcelModel
    {
        public String Ids { get; set; }
        [JsonIgnore]
        public String ApiUrl { get; set; }
        [JsonIgnore]
        public String TimeSpan { get => ((Int64)(new TimeSpan(DateTime.UtcNow.Ticks - (new DateTime(1970, 1, 1, 0, 0, 0).Ticks)).TotalMilliseconds)).ToString(); }
    }
    public class ScanCodeModel
    {
        public Guid Id { get; set; }
        public Int64 SCode { get; set; }
        public Int64 ECode { get; set; }
    }
}
