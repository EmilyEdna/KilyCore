﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using KilyCore.WEB.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.WEB.Util
{
    public class FileUtil
    {
        /// <summary>
        /// 上传文件针对图片
        /// </summary>
        /// <param name="Size"></param>
        /// <param name="Files"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static Object UploadFile(IFormFile Files, string FolderName, string WebRootPath)
        {
            string[] FileType = { ".jpg", ".png", ".jpeg", ".bmp", ".gif", ".ico" };
            //文件后缀
            var FileExtension = Path.GetExtension(Files.FileName);
            if (FileExtension == null)
                return new { data = "", flag = -1, msg = "文件没有后缀名！", HttpCode = 50 };
            if (!FileType.Contains(FileExtension))
                return new { data = "", flag = -1, msg = "文件类型不正确！", HttpCode = 50 };
            long bytes = Files.Length;
            string RootPath = $"/Upload/Images/{FolderName}/{DateTime.Now.ToString("yyyyMMdd")}/";
            string SavePath = WebRootPath + RootPath;
            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);
            string FullFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + FileExtension;
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
        public static IList<byte[]> CreatePDFBytes(Contract Params, string WebRootPath)
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
        public static byte[] HTMLToPDF(string WebRootPath, ContractHelp help)
        {
            string TemplatePath = WebRootPath + @"/Template/Template.html";
            StreamReader reader = new StreamReader(TemplatePath, Encoding.UTF8);
            string HTMLContent = reader.ReadToEnd();
            if (help.ContractType == 1)
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
        public static int Money(ContractHelp help) {
            try
            {
                return Convert.ToInt32(help.AttachInfo) * help.ContractYear;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
