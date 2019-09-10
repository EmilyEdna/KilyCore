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
        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayModel { get; set; }
    }
    public class FromData
    {
        public String Path { get; set; }
    }
    public static class Configer
    {
        public static String Host { get; set; }
        public static String WebHost { get; set; }
        public static String WebHostClass { get; set; }
        public static String WebHostBox { get; set; }

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
        public String Id { get; set; }
        public Int64 SCode => SCodes.Contains("W") ? Convert.ToInt64(SCodes.Split("W")[1]) : (SCodes.Contains("P") ? Convert.ToInt64(SCodes.Split("P")[1]) : Convert.ToInt64(SCodes.Split("B")[1]));
        public Int64 ECode => ECodes.Contains("W") ? Convert.ToInt64(ECodes.Split("W")[1]) : (ECodes.Contains("P") ? Convert.ToInt64(ECodes.Split("P")[1]) : Convert.ToInt64(ECodes.Split("B")[1]));
        public String SCodes { get; set; }
        public String ECodes { get; set; }
        public String CodeHost => ECodes.Contains("W") ? ECodes.Split("W")[0] + "W" : (ECodes.Contains("P") ? ECodes.Split("P")[0] + "P" : ECodes.Split("B")[0] + "B");

    }
    public class AmbientModel
    {
        /// <summary>
        /// 空气温度
        /// </summary>
        public string AirEnv { get; set; }
        /// <summary>
        /// 空气湿度
        /// </summary>
        public string AirHdy { get; set; }
        /// <summary>
        /// 土壤温度
        /// </summary>
        public string SoilEnv { get; set; }
        /// <summary>
        /// 土壤湿度
        /// </summary>
        public string SoilHdy { get; set; }
        /// <summary>
        /// 光照
        /// </summary>
        public string Light { get; set; }
        /// <summary>
        /// co2浓度
        /// </summary>
        public string CO2 { get; set; }
    }
    public class CreateImgHelper
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
    public class ApkVer {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Down { get; set; }
        public string Fix { get; set; }
        public string Category { get; set; }
    }
}
