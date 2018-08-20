using System;
using System.Collections.Generic;
using KilyCore.WEB.Model;
using KilyCore.WEB.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace KilyCore.WEB.Controllers
{
    public class FileController : Controller
    {
        public IHostingEnvironment Environment;
        public FileController(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="Size"></param>
        /// <param name="Files"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadImg(IFormFile Files, string FolderName)
        {
            var WebRootPath = Environment.WebRootPath;
            Object data = FileUtil.UploadFile(Files, FolderName, WebRootPath);
            return new JsonResult(data);
        }
        /// <summary>
        /// 下载PDF合同文件
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        [HttpPost]
        public FileResult CreatePDF(Contract Param)
        {
            var WebRootPath = Environment.WebRootPath;
            var result = FileUtil.CreatePDFBytes(Param, WebRootPath);
            var bytes = FileUtil.SavePDF(result);
            return File(bytes, "application/pdf", "入住合同.pdf");
        }
        /// <summary>
        /// 下载PDF合同文件
        /// </summary>
        /// <param name="help"></param>
        /// <returns></returns>
        [HttpPost]
        public FileResult HTMLToPDF(ContractHelp help)
        {
            var WebRootPath = Environment.WebRootPath;
            byte[] bytes = FileUtil.HTMLToPDF(WebRootPath, help);
            FileResult PDF = new FileContentResult(bytes, "application/pdf");
            PDF.FileDownloadName = "入住合同.pdf";
            return PDF;
        }
        /// <summary>
        /// 删除图片物理路径
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemovePath(FromData data)
        {
            var WebRootPath = Environment.WebRootPath;
            Object Result = FileUtil.RemovePath(data, WebRootPath);
            return new JsonResult(Result);
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public FileResult ExportExcel(ExcelModel datas)
        {
            string Path = Configer.Host + datas.ApiUrl;
            IDictionary<String, String> Map = new Dictionary<String, String>
            {
                { "Ids", "Id" }
            };
            var keyValuePairs = HttpClientUtil.KeyValuePairs(datas, Map);
            var Result = HttpClientUtil.HttpPostAsync(Path, keyValuePairs, null, "application/x-www-form-urlencoded").Result;
            var data = JsonConvert.DeserializeObject<MaterialStockIn>(Result);
            data.GetType().GetProperties().ToList();
            dynamic dynamics = JsonConvert.DeserializeObject<dynamic>(Result).data;
           var s= ((dynamics.First as JContainer).First as JProperty).Name;
            var count = (dynamics.First as JContainer).Count;
            for (int i = 0; i < count; i++)
            {

            }
            var bytes = FileUtil.ExportExcel(data.data, "报表", true);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "报表单.xlsx");
        }
    }
}