using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.UtilExtension
{
    /// <summary>
    /// 一般工具类
    /// </summary>
    public class NormalUtil
    {
        /// <summary>
        /// 创建随机数
        /// </summary>
        /// <param name="StarNo"></param>
        /// <param name="EndNo"></param>
        /// <returns></returns>
        public static string CreateRandomNum(int StarNo = 0, int EndNo = 10)
        {
            Random Rd = new Random();
            int Result = Rd.Next(StarNo, EndNo);
            return Result.ToString();
        }
        /// <summary>
        /// 判断是否为汉字
        /// </summary>
        /// <param name="text"></param>
        /// <returns>真：是汉字；假：不是</returns>
        public static bool CheckStringChineseUn(String Param)
        {
            bool result = false;
            foreach (char item in Param)
            {
                if (item >= 0x4e00 && item <= 0x9fbb)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
    public static class RegexExtension {
        public static List<string> ToList(this MatchCollection collection)
        {
            List<string> TempList = new List<string>();
            for (int i = 0; i < collection.Count; i++)
            {
                TempList.Add(collection[i].Value);
            }
            return TempList;
        }
    }
    #region 截取字符串 20190820
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class Extensions
    {
        #region PinYin(获取汉字的拼音简码)
        /// <summary>
        /// 获取汉字的拼音简码，即首字母缩写,范例：中国,返回zg
        /// </summary>
        /// <param name="str">汉字文本,范例： 中国</param>
        public static string ToPinYin(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            var result = new StringBuilder();
            foreach (char text in str)
                result.AppendFormat("{0}", ResolvePinYin(text));
            return result.ToString().ToLower();
        }

        /// <summary>
        /// 获取汉字的首字母拼音简码，即首字母缩写,范例：中国,返回z
        /// </summary>
        /// <param name="str">汉字文本,范例： 中国</param>
        public static string ToFirstPinYin(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;
            var result = new StringBuilder();
            foreach (char text in str)
                result.AppendFormat("{0}", ResolvePinYin(text));
            return result.ToString().ToLower();
        }

        /// <summary>
        /// 解析单个汉字的拼音简码
        /// </summary>
        /// <param name="text">单个汉字</param>
        private static string ResolvePinYin(char text)
        {
            byte[] charBytes = Encoding.Default.GetBytes(text.ToString());
            if (charBytes[0] <= 127)
                return text.ToString();
            var unicode = (ushort)(charBytes[0] * 256 + charBytes[1]);
            string pinYin = ResolvePinYinByCode(unicode);
            if (!string.IsNullOrWhiteSpace(pinYin))
                return pinYin;
            return text.ToString();
        }

        /// <summary>
        /// 使用字符编码方式获取拼音简码
        /// </summary>
        private static string ResolvePinYinByCode(ushort unicode)
        {
            if (unicode >= '\uB0A1' && unicode <= '\uB0C4')
                return "A";
            if (unicode >= '\uB0C5' && unicode <= '\uB2C0' && unicode != 45464)
                return "B";
            if (unicode >= '\uB2C1' && unicode <= '\uB4ED')
                return "C";
            if (unicode >= '\uB4EE' && unicode <= '\uB6E9')
                return "D";
            if (unicode >= '\uB6EA' && unicode <= '\uB7A1')
                return "E";
            if (unicode >= '\uB7A2' && unicode <= '\uB8C0')
                return "F";
            if (unicode >= '\uB8C1' && unicode <= '\uB9FD')
                return "G";
            if (unicode >= '\uB9FE' && unicode <= '\uBBF6')
                return "H";
            if (unicode >= '\uBBF7' && unicode <= '\uBFA5')
                return "J";
            if (unicode >= '\uBFA6' && unicode <= '\uC0AB')
                return "K";
            if (unicode >= '\uC0AC' && unicode <= '\uC2E7')
                return "L";
            if (unicode >= '\uC2E8' && unicode <= '\uC4C2')
                return "M";
            if (unicode >= '\uC4C3' && unicode <= '\uC5B5')
                return "N";
            if (unicode >= '\uC5B6' && unicode <= '\uC5BD')
                return "O";
            if (unicode >= '\uC5BE' && unicode <= '\uC6D9')
                return "P";
            if (unicode >= '\uC6DA' && unicode <= '\uC8BA')
                return "Q";
            if (unicode >= '\uC8BB' && unicode <= '\uC8F5')
                return "R";
            if (unicode >= '\uC8F6' && unicode <= '\uCBF9')
                return "S";
            if (unicode >= '\uCBFA' && unicode <= '\uCDD9')
                return "T";
            if (unicode >= '\uCDDA' && unicode <= '\uCEF3')
                return "W";
            if (unicode >= '\uCEF4' && unicode <= '\uD188')
                return "X";
            if (unicode >= '\uD1B9' && unicode <= '\uD4D0')
                return "Y";
            if (unicode >= '\uD4D1' && unicode <= '\uD7F9')
                return "Z";
            return string.Empty;
        }

        #endregion

        #region 删除最后结尾的指定字符后的字符
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(this string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }
        #endregion

        #region 去除HTML标记
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="NoHTML">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHtml(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            //删除脚本
            str = Regex.Replace(str, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            str = Regex.Replace(str, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"-->", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"<!--.*", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&hellip;", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&mdash;", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&ldquo;", "", RegexOptions.IgnoreCase);
            str.Replace("<", "");
            str = Regex.Replace(str, @"&rdquo;", "", RegexOptions.IgnoreCase);
            str.Replace(">", "");
            str.Replace("\r\n", "");
            str = str.Trim();
            return str;
        }

        /// <summary>
        /// 去除HTML标记并截取字符串
        /// </summary>
        /// <param name="NoHTML">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHtml(this string str, int len, string lastStr = "…")
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            return str.NoHtml().CutString(len, lastStr);
        }
        #endregion

        #region 截取字符长度
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="inputString">字符</param>
        /// <param name="len">长度</param>
        /// <param name="lastStr">超过长度增加指定符号，如不指定则默认加…</param>
        /// <returns></returns>
        public static string CutString(this string inputString, int len, string lastStr = "…")
        {
            if (string.IsNullOrEmpty(inputString))
                return "";
            inputString = inputString.NoHtml();
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上指定符号
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += lastStr;
            return tempString;
        }
        #endregion

        #region TXT代码转换成HTML格式
        /// <summary>
        /// 把TXT代码转换成HTML格式
        /// </summary>
        public static string ToHtml(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("'", "&apos;");
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br />");
            sb.Replace("\n", "<br />");
            sb.Replace("\t", " ");
            //sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }
        #endregion

        #region HTML代码转换成TXT格式
        /// <summary>
        /// 把HTML代码转换成TXT格式
        /// </summary>
        public static string ToTxt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<br />", "\n");
            sb.Replace("<br />", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }
        #endregion
    }
    #endregion
}
