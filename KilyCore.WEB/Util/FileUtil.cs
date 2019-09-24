using iTextSharp.text;
using iTextSharp.text.pdf;
using KilyCore.WEB.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PuppeteerSharp;
using SelectPdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.WEB.Util
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    public class FileUtil
    {
        /// <summary>
        /// 上传文件针对图片
        /// </summary>
        /// <param name="Size"></param>
        /// <param name="Files"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static Object UploadFile(IFormFile Files, String FolderName, String WebRootPath)
        {
            String[] FileType = { ".jpg", ".png", ".jpeg", ".gif" };
            //文件后缀
            var FileExtension = Path.GetExtension(Files.FileName);
            if (FileExtension == null)
                return new { data = "", flag = -1, msg = "文件没有后缀名！", HttpCode = 50 };
            if (!FileType.Contains(FileExtension))
                return new { data = "", flag = -1, msg = "文件类型不正确！", HttpCode = 50 };
            long bytes = Files.Length;
            String RootPath = $"/Upload/Images/{FolderName}/{DateTime.Now.ToString("yyyyMMdd")}/";
            String SavePath = WebRootPath + RootPath;
            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);
            String FullFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + FileExtension;
            using (FileStream fs = File.Create(SavePath + FullFileName))
            {
                Files.CopyTo(fs);
                fs.Flush();
            }
            return new { data = RootPath + FullFileName, flag = 1, msg = "上传成功！", HttpCode = 10 };
        }
        /// <summary>
        /// 导入xmls
        /// </summary>
        /// <param name="Files"></param>
        /// <param name="Type">1进货2销售</param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static Object ImportXmls(IFormFile Files, int Type, string WebRootPath)
        {
            try
            {
            if (!Directory.Exists(WebRootPath+"/TempExcel/"))
                Directory.CreateDirectory(WebRootPath + "/TempExcel/");
            using (FileStream fs = File.Create(WebRootPath + "/TempExcel/"+Files.FileName))
            {
                Files.CopyTo(fs);
                fs.Flush();
            }
            FileInfo file = new FileInfo(WebRootPath + "/TempExcel/" + Files.FileName);
            if (file != null)
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    //获取表格的列数和行数
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (Type == 1)
                        {
                            RepastBuybill bill = new RepastBuybill
                            {
                                GoodsName = worksheet.Cells[row, 1].Value.ToString(),
                                GoodsNum = worksheet.Cells[row, 2].Value.ToString(),
                                Unit = worksheet.Cells[row, 3].Value.ToString(),
                                UnPay = worksheet.Cells[row, 4].Value.ToString(),
                                ToPay = worksheet.Cells[row, 5].Value.ToString(),
                                Supplier = worksheet.Cells[row, 6].Value.ToString(),
                                LinkPhone = worksheet.Cells[row, 7].Value.ToString(),
                                NoExp = worksheet.Cells[row, 8].Value.ToString(),
                                ProTime = worksheet.Cells[row, 9].Value.ToString() == "-" ? DateTime.Now : DateTime.Parse(worksheet.Cells[row, 8].Value.ToString()),
                                Purchase = worksheet.Cells[row, 10].Value.ToString(),
                                OrderTime = worksheet.Cells[row, 11].Value.ToString() == "-" ? DateTime.Now : DateTime.Parse(worksheet.Cells[row, 11].Value.ToString()),
                            };
                            var keyValuePairs = HttpClientUtil.KeyValuePairs(bill);
                            String Result = HttpClientUtil.HttpPostAsync(Configer.Host + "RepastWeb/EditBuybill", keyValuePairs, null, "application/x-www-form-urlencoded").Result;
                        }
                        else {
                            RepastSellbill bill = new RepastSellbill
                            {
                                GoodsName = worksheet.Cells[row, 1].Value.ToString(),
                                GoodsNum = worksheet.Cells[row, 2].Value.ToString(),
                                UnPay= worksheet.Cells[row, 3].Value.ToString(),
                                ToPay = worksheet.Cells[row, 4].Value.ToString(),
                                NoExp = worksheet.Cells[row, 5].Value.ToString(),
                                ProTime = worksheet.Cells[row, 6].Value.ToString() == "-" ? DateTime.Now : DateTime.Parse(worksheet.Cells[row, 6].Value.ToString()),
                                SellTime= worksheet.Cells[row, 7].Value.ToString() == "-" ? DateTime.Now : DateTime.Parse(worksheet.Cells[row, 7].Value.ToString()),
                                Manager= worksheet.Cells[row, 8].Value.ToString(),
                            };
                            var keyValuePairs = HttpClientUtil.KeyValuePairs(bill);
                            String Result = HttpClientUtil.HttpPostAsync(Configer.Host + "RepastWeb/EditSellbill", keyValuePairs, null, "application/x-www-form-urlencoded").Result;
                        }
                    }
                }
            }
            File.Delete(WebRootPath + "/TempExcel/" + Files.FileName);
            return "导入成功";
            }
            catch (Exception)
            {
                return "导入失败";
            }
        }
        /// <summary>
        /// 上传音频
        /// </summary>
        /// <param name="Files"></param>
        /// <param name="FolderName"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static Object UploadOther(IFormFile Files, String FolderName, String WebRootPath)
        {
            long bytes = Files.Length;
            String RootPath = $"/Upload/Others/{FolderName}/{DateTime.Now.ToString("yyyyMMdd")}/";
            String SavePath = WebRootPath + RootPath;
            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);
            using (FileStream fs = File.Create(SavePath + Files.FileName))
            {
                Files.CopyTo(fs);
                fs.Flush();
            }
            return new { data = RootPath + Files.FileName, flag = 1, msg = "上传成功！", HttpCode = 10 };
        }
        /// <summary>
        /// 创建PDF文件流
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="Version"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        [Obsolete]
        public static IList<byte[]> CreatePDFBytes(Contract Params, String WebRootPath)
        {
            IDictionary<Object, Object> Map = new Dictionary<Object, Object>();
            IList<byte[]> Lbyte = new List<byte[]>();
            Params.GetType().GetProperties().ToList().ForEach(t =>
            {
                Map.Add(t.Name, t.GetValue(Params, null));
            });
            var TemplatePath = WebRootPath + @"\Template\";
            //创建中文字体
            BaseFont baseFont = BaseFont.CreateFont($"{TemplatePath}SIMSUNB.ttc,0", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //读取PDF模板
            PdfReader reader = new PdfReader($"{TemplatePath}Template.pdf");
            //创建内存流保存
            MemoryStream memory = new MemoryStream();
            PdfStamper PDF = new PdfStamper(reader, memory);
            //获取PDF表单
            AcroFields form = PDF.AcroFields;
            //表单文本框是否锁定
            PDF.FormFlattening = true;
            //填充表单,para为表单的一个（属性-值）字典
            foreach (KeyValuePair<Object, Object> Param in Map)
            {
                //要输入中文就要设置域的字体;
                form.SetFieldProperty(Param.Key.ToString(), "textfont", baseFont, null);
                //为需要赋值的域设置值;
                form.SetField(Param.Key.ToString(), Param.Value.ToString());
            }
            //关闭PDF流
            PDF.Close();
            reader.Close();
            //将流转换成字节
            Lbyte.Add(memory.ToArray());
            return Lbyte;
        }
        /// <summary>
        /// PDF内存流
        /// </summary>
        /// <param name="Lbyte"></param>
        /// <returns></returns>
        [Obsolete]
        public static byte[] SavePDF(IList<byte[]> Lbyte)
        {
            Document document = new Document();
            MemoryStream memory = new MemoryStream();
            //这里用的是smartCopy，整篇文档只会导入一份字体。属于可接受范围内
            PdfSmartCopy copy = new PdfSmartCopy(document, memory);
            document.Open();
            PdfReader reader = new PdfReader(Lbyte.FirstOrDefault());
            document.NewPage();
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                //for循环新增文档页数，并copy pdf数据
                PdfImportedPage imported = copy.GetImportedPage(reader, i);
                copy.AddPage(imported);
            }
            reader.Close();
            copy.Close();
            document.Close();
            return memory.ToArray();
        }
        /// <summary>
        /// HTML合同模板转PDF
        /// </summary>
        /// <param name="WebRootPath"></param>
        /// <param name="help"></param>
        /// <returns></returns>
        public static byte[] HTMLToPDF(String WebRootPath, ContractHelp help)
        {
            String TemplatePath = WebRootPath + @"/Template/Template.html";
            StreamReader reader = new StreamReader(TemplatePath, Encoding.UTF8);
            String HTMLContent = reader.ReadToEnd();
            String PayModel = String.Empty;
            if (help.PayModel == 10)
                PayModel = "支付宝";
            else if (help.PayModel == 20)
                PayModel = "微信";
            else if (help.PayModel == 30)
                PayModel = "银行转账";
            else if (help.PayModel == 40)
                PayModel = "自定义支付-标签购买";
            HTMLContent = HTMLContent.Replace("{CompanyCategory}", help.CompanyName)
                .Replace("{PathNo}", help.PathNo)
                .Replace("{CompanyName}", help.CompanyName)
                .Replace("{CompanyCode}", help.CommunityCode)
                .Replace("{CompanyAddress}", help.CompanyAddress)
                .Replace("{StartTimeYear}", help.StarYear.ToString())
                .Replace("{StartTimeMonth}", help.StarMonth.ToString())
                .Replace("{StartTimeDay}", help.StarDay.ToString())
                .Replace("{EndTimeYear}", help.EndYear.ToString())
                .Replace("{EndTimeMonth}", help.EndMonth.ToString())
                .Replace("{EndTimeDay}", help.EndDay.ToString())
                .Replace("{Years}", help.ContractYear.ToString())
                .Replace("{CompanyVersion}", help.VersionName)
                .Replace("{Moneys}", Money(help).ToString())
                .Replace("{MeneyUp}", XExten.XPlus.XPlusEx.XConvertCHN(Money(help)))
                .Replace("{ServerItems}", help.VersionDes)
                .Replace("{PayModel}", PayModel);
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfStandard = PdfStandard.Full;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
            converter.Options.EmbedFonts = true;
            converter.Options.MarginLeft = 60;
            converter.Options.MarginRight = 60;
            converter.Options.MarginTop = 60;
            converter.Options.MarginBottom = 60;

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(HTMLContent);
            byte[] bytes = doc.Save();
            doc.Close();
            return bytes;
        }
        /// <summary>
        /// 计算版本价格
        /// </summary>
        /// <param name="help"></param>
        /// <returns></returns>
        public static int Money(ContractHelp help)
        {
            try
            {
                var price = help.AttachInfo.Replace("元", "").Trim();
                return Convert.ToInt32(price) * help.ContractYear;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除图片物理路径
        /// </summary>
        /// <param name="Param"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static String RemovePath(FromData Param, String WebRootPath)
        {
            String Path = Param.Path;
            if (String.IsNullOrEmpty(Path))
                return null;
            //针对富文本图片删除
            if (Path.Contains("img"))
            {
                Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
                MatchCollection matches = regImg.Matches(Path);
                //遍历所有的img标签对象
                foreach (Match match in matches)
                {
                    File.Delete(WebRootPath + match.Groups["imgUrl"].Value);
                }
            }
            else
            {
                if (Path.Contains(","))
                    Path.Split(",").ToList().ForEach(t =>
                    {
                        File.Delete(WebRootPath + t);
                    });
                else
                    File.Delete(WebRootPath + Path);
            }
            return null;
        }
        /// <summary>
        /// 导出二维码地址模式一
        /// </summary>
        /// <param name="model"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static byte[] ExportTxt(ScanCodeModel model, String WebRootPath)
        {
            Int64 region = model.ECode - model.SCode;
            IList<String> Address = new List<String>();
            String Content = String.Empty;
            String FileName = String.Empty;
            if (string.IsNullOrEmpty(model.Id))
                model.Id = null;
            if (!model.CodeHost.Substring(2, 1).Contains("B"))
            {
                if (model.CodeHost.Substring(2, 1).Contains("W"))
                {
                    for (long i = region; i > 0; i--)
                    {
                        Address.Add(string.Format(Configer.WebHost, model.Id, HttpUtility.UrlEncode(model.CodeHost) + (model.SCode + i) + GetRandom()));
                    }
                    Address.Add(string.Format(Configer.WebHost, model.Id, HttpUtility.UrlEncode(model.CodeHost) + model.SCode + GetRandom()));
                }
                else
                {
                    for (long i = region; i > 0; i--)
                    {
                        Address.Add(string.Format(Configer.WebHostClass, model.Id, HttpUtility.UrlEncode(model.CodeHost) + (model.SCode + i) + GetRandom()));
                    }
                    Address.Add(string.Format(Configer.WebHostClass, model.Id, HttpUtility.UrlEncode(model.CodeHost) + model.SCode + GetRandom()));
                }
                Content = String.Join("\r\n", Address);
                FileName = WebRootPath + @"\Template\ScanLink.txt";
            }
            else
            {
                for (long i = region; i > 0; i--)
                {
                    Address.Add(string.Format(Configer.WebHostBox, model.Id, HttpUtility.UrlEncode(model.CodeHost) + (model.SCode + i) + GetRandom()));
                }
                Address.Add(string.Format(Configer.WebHostBox, model.Id, HttpUtility.UrlEncode(model.CodeHost) + model.SCode + GetRandom()));
                Content = String.Join("\r\n", Address);
                FileName = WebRootPath + @"\Template\ScanLinkBox.txt";
            }
            using (StreamWriter str = File.CreateText(FileName))
            {
                str.WriteLine(Content);
            }
            using (FileStream Fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[Fs.Length];
                Fs.Read(bytes, 0, (int)Fs.Length);
                return bytes;
            }
        }
        /// <summary>
        /// 导出二维码地址模式二
        /// </summary>
        /// <param name="model"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static byte[] ExportTxts(ScanCodeModel model, String WebRootPath)
        {
            Int64 region = model.ECode - model.SCode;
            IList<String> Address = new List<String>();
            String Content = String.Empty;
            String FileName = String.Empty;
            if (string.IsNullOrEmpty(model.Id))
                model.Id = null;
            for (long i = region; i > 0; i--)
            {
                var n = GetRandom();
                var code = string.Format(Configer.WebHost, model.Id, HttpUtility.UrlEncode(model.CodeHost) + (model.SCode + i) + n) + "," + (model.CodeHost + (model.SCode + i) + n);
                Address.Add(code);
            }
            var ns = GetRandom();
            var codes = string.Format(Configer.WebHost, model.Id, HttpUtility.UrlEncode(model.CodeHost) + model.SCode + ns) + "," + ((model.CodeHost + model.SCode) + ns);
            Address.Add(codes);
            Content = String.Join("\r\n", Address);
            FileName = WebRootPath + @"\Template\ScanLink.txt";
            using (StreamWriter str = File.CreateText(FileName))
            {
                str.WriteLine(Content);
            }
            using (FileStream Fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[Fs.Length];
                Fs.Read(bytes, 0, (int)Fs.Length);
                return bytes;
            }
        }
        /// <summary>
        /// 导出二维码地址商家
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static byte[] ExportMerTxt(String Id, String WebRootPath)
        {
            String FileName = String.Empty;
            String MerchantCode = $@"http://www.cfda.vip/Wap/RepastCode?DataSource=0&Id={Id}";
            FileName = WebRootPath + @"\Template\MerchantLink.txt";
            using (StreamWriter str = File.CreateText(FileName))
            {
                str.WriteLine(MerchantCode);
            }
            using (FileStream Fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[Fs.Length];
                Fs.Read(bytes, 0, (int)Fs.Length);
                return bytes;
            }
        }
        /// <summary>
        /// 导出Excel-EPPLUS
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName">列明</param>
        /// <param name="Data">数据源</param>
        /// <param name="WorkSheetName">工作簿名称</param>
        /// <param name="ShowNo">是否显示编号</param>
        /// <returns></returns>
        public static byte[] ExportExcel<T>(List<T> Data, String WorkSheetName = null, bool ShowNo = false, List<String> ColName = null)
        {
            byte[] Result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                //添加工作簿
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(WorkSheetName);
                //是否显示行编号
                if (ShowNo)
                {
                    int index = 1;
                    Data.ForEach(t =>
                    {
                        t.GetType().GetProperty("编号").SetValue(t, index);
                        index++;
                    });
                }
                //从A1开始添加数据
                workSheet.Cells["A1"].LoadFromCollection(Data, true);
                workSheet.Cells.AutoFitColumns();
                //设置表格样式
                using (ExcelRange rng = workSheet.Cells[1, 1, Data.Count + 1, Data[0].GetType().GetProperties().Count()])
                {
                    rng.Style.Font.Name = "宋体";
                    rng.Style.Font.Size = 10;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));

                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(155, 155, 155));

                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(155, 155, 155));

                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(155, 155, 155));
                }
                using (ExcelRange rng = workSheet.Cells[1, 1, 1, Data[0].GetType().GetProperties().Count()])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(234, 241, 246));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.FromArgb(51, 51, 51));
                }
                // 删除忽略的列
                int flag = 0;
                if (ColName != null)
                    Data.FirstOrDefault().GetType().GetProperties().ToList().ForEach(t =>
                    {
                        flag += 1;
                        if (!ColName.Contains(t.Name))
                            workSheet.DeleteColumn(flag);
                    });
                Result = package.GetAsByteArray();
            }
            return Result;
        }
        /// <summary>
        /// 导出Excel-EPPLUS
        /// </summary>
        /// <param name="Data">数据源</param>
        /// <param name="WorkSheetName">工作簿名称</param>
        /// <returns></returns>
        public static byte[] ExportExcel(DataTable Data, String WorkSheetName = null, List<String> ColName = null)
        {
            byte[] Result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                //添加工作簿
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(WorkSheetName);
                //从A1开始添加数据
                workSheet.Cells["A1"].LoadFromDataTable(Data, true);
                workSheet.Cells.AutoFitColumns();
                //设置表格样式
                using (ExcelRange rng = workSheet.Cells[1, 1, Data.Rows.Count + 1, Data.Columns.Count])
                {
                    rng.Style.Font.Name = "宋体";
                    rng.Style.Font.Size = 10;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));

                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(155, 155, 155));

                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(155, 155, 155));

                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(155, 155, 155));
                }
                using (ExcelRange rng = workSheet.Cells[1, 1, 1, Data.Columns.Count])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(234, 241, 246));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.FromArgb(51, 51, 51));
                }
                //删除忽略列
                for (int i = Data.Columns.Count - 1; i >= 0; i--)
                {
                    if (ColName != null)
                    {
                        if (ColName.Contains(Data.Columns[i].ColumnName))
                            workSheet.DeleteColumn(i + 1);
                    }
                }
                Result = package.GetAsByteArray();
            }
            return Result;
        }
        /// <summary>
        /// JSON转DataTable
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public static DataTable ConvertJsonToDatatable(String Param)
        {
            DataTable dt = new DataTable();
            ArrayList arrayList = JsonConvert.DeserializeObject<ArrayList>(Param);
            ArrayList array = new ArrayList();
            if (arrayList.Count > 0)
            {
                for (int i = 0; i < arrayList.Count; i++)
                {
                    Dictionary<String, Object> Map = JsonConvert.DeserializeObject<Dictionary<String, Object>>(JsonConvert.SerializeObject(arrayList[i]));
                    if (Map.Keys.Contains("编号"))
                        Map["编号"] = i + 1;
                    array.Add(Map);
                }
                foreach (Dictionary<String, Object> dictionary in array)
                {
                    if (dictionary.Keys.Count() == 0)
                        return dt;
                    //列
                    if (dt.Columns.Count == 0)
                        foreach (string current in dictionary.Keys)
                        {
                            dt.Columns.Add(current, dictionary[current].GetType());
                        }
                    //行
                    DataRow dataRow = dt.NewRow();
                    foreach (string current in dictionary.Keys)
                    {
                        dataRow[current] = dictionary[current];
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }
        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <returns></returns>
        public static int GetRandom()
        {
            Random rd = new Random();
            return rd.Next(1, 10);
        }
        /// <summary>
        /// 将网页保存为图片
        /// </summary>
        /// <param name="data"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static async Task<Object> GetPageToImage(CreateImgHelper data, String WebRootPath)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new string[] { "--no-sandbox" }
            });
            var page = await browser.NewPageAsync();
            bool fullPage = true;
            if (data.Width.HasValue && data.Height.HasValue)
            {
                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = data.Width.Value,
                    Height = data.Height.Value
                });
                fullPage = false;
            }
            await page.GoToAsync(HttpUtility.UrlDecode($"{data.Path}?Id={data.Id}&Type=Img"));
            string Path = WebRootPath + "/Upload/Images/";
            string fileName = $"{Guid.NewGuid().ToString()}.png";
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
            await page.ScreenshotAsync($"{Path + fileName}", new ScreenshotOptions { FullPage = fullPage, Type = ScreenshotType.Png });
            return new { data = "http://system.cfda.vip/Upload/Images/" + fileName, flag = 1, msg = "生成成功！", HttpCode = 10 };
        }
        /// <summary>
        /// APK版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="WebRootPath"></param>
        /// <param name="section"></param>
        public static string ApkVer(List<ApkVer> t, string WebRootPath, string section = "")
        {
            string Path = WebRootPath + "\\Config\\Apk.json";
            if (Directory.Exists(WebRootPath))
                Directory.CreateDirectory(WebRootPath + "\\Config");
            if (!File.Exists(Path))
            {
                FileStream fs = new FileStream(Path, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                ApkVers data = new ApkVers() { data = t };
                sw.WriteLine(JsonConvert.SerializeObject(data));
                sw.Close();
                fs.Close();
            }
            else
            {
                var streams = new FileStream(Path, FileMode.Open);
                StreamReader reader = new StreamReader(streams);
                var result = reader.ReadToEnd();
                var entity = JsonConvert.DeserializeObject<ApkVers>(result);
                reader.Close();
                streams.Close();
                FileStream fs = File.Open(Path, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Seek(0, SeekOrigin.Begin);
                fs.SetLength(0);
                entity.data.AddRange(t);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(JsonConvert.SerializeObject(entity));
                sw.Close();
                fs.Close();
            }
            return "/Config/Apk.json";
        }
        /// <summary>
        /// 上传APK
        /// </summary>
        /// <param name="Files"></param>
        /// <param name="FolderName"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static string UploadAPK(IFormFile Files, String WebRootPath)
        {
            String RootPath = "/Config/";
            String SavePath = WebRootPath + RootPath;
            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);
            var replay = Guid.NewGuid().ToString() + "_";
            using (FileStream fs = File.Create(SavePath + replay + Files.FileName))
            {
                Files.CopyTo(fs);
                fs.Flush();
            }
            return RootPath + replay + Files.FileName;
        }
    }
}
