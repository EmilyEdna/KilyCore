using KilyCore.Extension.HttpClientFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var companyName = Regex.Matches(html, "<h2\\s\\w+=\"(.*?)\">.*?<").Select(item => new
            {
                Name = Regex.Match(item.Value, "：(.*?)<").Value.Split("<")[0].Split("：")[1]
            }).ToList();
            var companyInfo = Regex.Matches(html, "<span\\s\\w+=\"desc-item-value\">.*?<").Select(item => new
            {
                Data = Regex.Match(item.Value, ">(.*?)<").Value.Split(">")[1].Split("<")[0]
            }).ToList();
            List<ProductInfoModel> lm = new List<ProductInfoModel>();
            int y = 0;
            for (int i = 0; i < companyInfo.Count; i++)
            {
                if (i % 4 == 0)
                {
                    if (i != 0)
                        y += 1;
                    lm.Add(new ProductInfoModel
                    {
                        ComName = companyName[y].Name,
                        QS = companyInfo[i].Data,
                        ProName = companyInfo[i + 1].Data,
                        Time = companyInfo[i + 2].Data,
                        Addr = companyInfo[i + 3].Data
                    });
                }
            }
            return lm;
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
            public string QS { get; set; }
            public string ProName { get; set; }
            public string Time { get; set; }
            public string Addr { get; set; }
        }
    }
}
