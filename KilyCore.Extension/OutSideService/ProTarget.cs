using KilyCore.Extension.HttpClientFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KilyCore.Extension.OutSideService
{
    public class ProTarget
    {
        public static Object GetTargetDb(string pageIndex, string keyword)
        {
            var keys = HttpClientExtension.KeyValuePairs<Object>(new { pageIndex, keyword, task = "listSearch_Website" });
            var html = HttpClientExtension.HttpPostAsync("https://sppt.cfsa.net.cn:8086/db", null, keys).Result;
            var Titil = Regex.Matches(html, "<a(.*?)onclick=\"g(.*?)\">(.*?)</a").Select(item => new
            {
                data = Regex.Replace(Regex.Replace(Regex.Replace(item.Value, "<font(.*?)>(.*?)</font>", keyword), "<a(.*?)onclick=\"(.*?)\">", ""), "</a", "").Replace(" ", "")
            }).ToList();
            var content = Regex.Matches(html.Replace("\n", "").Replace("\r", "").Replace("\t", ""), "<span class=\"list_zt\">(.*?)</span>").Select(t => new
            {
                data = t.Value.Replace("<span class=\"list_zt\">", "").Replace("</span>", "").Replace("<i>", "").Replace("</i>", ",").Replace("00:00:00.0", "")
            }).ToList();
            var remark = Regex.Matches(html.Replace("\n", "").Replace("\r", "").Replace("\t", ""), "<span class=\"list_p\">(.*?)</span>").Select(t => new
            {
                data = Regex.Replace(Regex.Replace(t.Value, "<i>(.*?)</i>", ""), "<font(.*?)>(.*?)</font>", keyword).Replace("<span class=\"list_p\">", "").Replace("</span>", "").Replace("&nbsp;", "").Replace("<br/>", "")
            }).ToList();
            var Temp = Regex.Match(Regex.Match(html.Replace("\n", "").Replace("\r", "").Replace("\t", ""), "<span align=\"left\" style=\"(.*?)\">(.*?)</span>").Value, "</font>(.*?)</span>").Value;
            var Page = Regex.Match(Temp, "\\d+").Value;
            List<object> lo = new List<object>();
            for (int i = 0; i < Titil.Count; i++)
            {
                lo.Add(new { Title = Titil[i].data, Content = content[i].data, Remark = remark[i].data });
            }
            return new { lo, Page };
        }
    }
}
