using System;
using KilyCore.WEB.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public JsonResult UploadImg(int Size, IFormFile Files)
        {
            var WebRootPath = Environment.WebRootPath;
            Object data = FileUtil.UploadFile(Size, Files, WebRootPath);
            return new JsonResult(data);
        }
        /// <summary>
        /// 下载PDF合同文件
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        [HttpPost]
        public FileResult CreatePDF(string CompanyName, string Version)
        {
            var WebRootPath = Environment.WebRootPath;
            string VersionName = string.Empty;
            if (Version == "10")
                VersionName = "体验版";
            if (Version == "20")
                VersionName = "基础版";
            if (Version == "30")
                VersionName = "升级版";
            if (Version == "40")
                VersionName = "旗舰版";
            var result = FileUtil.CreatePDFBytes(CompanyName, VersionName, WebRootPath);
            var bytes = FileUtil.SavePDF(result);
            FileResult PDF = new FileContentResult(bytes, "application/pdf");
            PDF.FileDownloadName = "入住合同.pdf";
            return PDF;
        }
    }
}