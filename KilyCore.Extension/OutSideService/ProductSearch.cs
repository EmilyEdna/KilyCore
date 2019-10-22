using KilyCore.Extension.HttpClientFactory;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace KilyCore.Extension.OutSideService
{
    public class ProductSearch
    {
        public static object GetProList(string name)
        {
            var param = HttpUtility.UrlEncode(HttpUtility.UrlEncode(name));
            var html = HttpClientExtension.HttpGetAsync($"http://spscxk.gsxt.gov.cn/spscxk/spscxkindexquery.xhtml?qymc={param}&fzjg=&zsbh=&lb=").Result;
            var datas = Regex.Matches(html.Replace("\r", "").Replace("\n", ""), "<li class=\"search-result-item\"(.*?)</li>").Select(item => new ProductInfoModel
            {
                ComName = Regex.Match(Regex.Match(item.Value, "<h2\\s\\w+=\"(.*?)\">.*?<").Value, "：(.*?)<").Value.Split("<")[0].Split("：")[1],
                QS = CheckNum(Regex.Matches(Regex.Matches(item.Value, "<div class=\"result-item-desc\">(.*?)</div>")[0].Value, "<span\\s\\w+=\"desc-item-value\">.*?<"), 0),
                ProName= CheckNum(Regex.Matches(Regex.Matches(item.Value, "<div class=\"result-item-desc\">(.*?)</div>")[0].Value, "<span\\s\\w+=\"desc-item-value\">.*?<"), 1),
                Time = CheckNum(Regex.Matches(Regex.Matches(item.Value, "<div class=\"result-item-desc\">(.*?)</div>")[0].Value, "<span\\s\\w+=\"desc-item-value\">.*?<"), 2),
                Addr= CheckNum(Regex.Matches(Regex.Matches(item.Value, "<div class=\"result-item-desc\">(.*?)</div>")[1].Value, "<span\\s\\w+=\"desc-item-value\">.*?<"), 0),
            }).ToList();
            return datas;
        }

        private static string CheckNum(MatchCollection Col, int index)
        {
            if (Col.Count == 2)
            {
                if (index != 1)
                {
                    if (index == 2)
                        index = index-1;
                    return Regex.Match(Col[index].Value, ">(.*?)<").Value.Split(">")[1].Split("<")[0];
                }
                else {
                    return "";
                }
            }
            else
                return Regex.Match(Col[index].Value, ">(.*?)<").Value.Split(">")[1].Split("<")[0];
            
        }



        public static object GetProDetail(string qs)
        {
            var detailhtml = HttpClientExtension.HttpGetAsync($"http://spscxk.gsxt.gov.cn/spscxk/detail.xhtml?zsbh={qs}&lb=QS").Result;
            return Regex.Matches(detailhtml, "<td>(.*?)<").Select(t => new
            {
                result = Regex.Match(t.Value, ">(.*?)<").Value.Split(">")[1].Split("<")[0]
            }).ToList();
        }

        public class ProductInfoModel
        {
            public string ComName { get; set; }
            public string ProName { get; set; }
            public string QS { get; set; }
            public string Time { get; set; }
            public string Addr { get; set; }
        }
    }
}