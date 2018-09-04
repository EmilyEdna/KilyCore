using iTextSharp.text;
using iTextSharp.text.pdf;
using KilyCore.WEB.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
            String[] FileType = { ".jpg", ".png", ".jpeg", ".bmp", ".gif", ".ico" };
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
        /// 创建PDF文件流
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="Version"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
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
            if (help.ContractType == 1&& help.AuthorCompany=="超级管理员")
                HTMLContent = HTMLContent.Replace("{CompanySelf}", Configer.CompanySelf)
                    .Replace("{CodeSelf}", Configer.CodeSelf)
                    .Replace("{AddressSelf}", Configer.AddressSelf)
                    .Replace("{Chapter}", Configer.Chapter)
                    .Replace("{CompanyCategory}", help.CompanyName)
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
                    .Replace("{ServerItems}", help.VersionDes);
            else
                HTMLContent = HTMLContent.Replace("{CompanySelf}", help.AuthorCompany)
                   .Replace("{CodeSelf}", help.Code)
                   .Replace("{Chapter}", help.Chapter)
                   .Replace("{AddressSelf}", help.Address)
                   .Replace("{CompanyCategory}", help.CompanyName)
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
                   .Replace("{ServerItems}", help.VersionDes);
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
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
                return Convert.ToInt32(help.AttachInfo) * help.ContractYear;
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
                    if (!ColName.Contains(Data.Columns[i].ColumnName))
                        workSheet.DeleteColumn(i + 1);
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
    }
}
