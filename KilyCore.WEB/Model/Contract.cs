using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public int Year{ get => 1; }
    }
}
