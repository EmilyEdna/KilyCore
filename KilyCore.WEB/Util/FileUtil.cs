using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public static Object UploadFile(int Size, IFormFile Files, string WebRootPath)
        {
            string[] FileType = { ".jpg", ".png", ".jpeg", ".bmp", ".gif", ".ico" };
            //文件后缀
            var FileExtension = Path.GetExtension(Files.FileName);
            if (FileExtension == null)
                return new { data = "", flag = -1, msg = "文件没有后缀名！", HttpCode = 50 };
            if (!FileType.Contains(FileExtension))
                return new { data = "", flag = -1, msg = "文件类型不正确！", HttpCode = 50 };
            long bytes = Files.Length;
            if (Size != 0)
                if (bytes > 1024 * 1024 * Size)
                    return new { data = "", flag = -1, msg = $"请限制图片大小在{Size}M以内！", HttpCode = 50 };
            if (bytes > 1024 * 1024 * 2) //2M
                return new { data = "", flag = -1, msg = "请限制图片大小在2M以内！", HttpCode = 50 };
            string RootPath = $"/Upload/Images/{DateTime.Now.ToString("yyyyMMdd")}/";
            string SavePath = WebRootPath + RootPath;
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
        public static IList<byte[]> CreatePDFBytes(string CompanyName, string Version, string WebRootPath)
        {
            IDictionary<Object, Object> Map = new Dictionary<Object, Object>();
            IList<byte[]> Lbyte = new List<byte[]>();
            Map.Add("No", $"YCKJ{DateTime.Now.ToString("yyyyMMdd")}");
            Map.Add("CompanyName", CompanyName);
            Map.Add("StartYear", DateTime.Now.Year);
            Map.Add("StartMonth", DateTime.Now.Month);
            Map.Add("StartDay", DateTime.Now.Day);
            Map.Add("EndYear", DateTime.Now.AddYears(1).Year);
            Map.Add("EndMonth", DateTime.Now.Month);
            Map.Add("EndDay", DateTime.Now.Day);
            Map.Add("Version", Version);
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
            foreach (byte[] bytes in Lbyte)
            {
                PdfReader reader = new PdfReader(bytes);
                //for循环新增文档页数，并copy pdf数据
                document.NewPage();
                PdfImportedPage imported = copy.GetImportedPage(reader, 1);
                copy.AddPage(imported);
                reader.Close();
            }
            copy.Close();
            document.Close();
            return memory.ToArray();
        }
    }
}
