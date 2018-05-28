using System;
using KilyCore.WEB.Model;
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
        public FileResult CreatePDF(Contract Param)
        {
            var WebRootPath = Environment.WebRootPath;
            var result = FileUtil.CreatePDFBytes(Param, WebRootPath);
            var bytes = FileUtil.SavePDF(result);
            FileResult PDF = new FileContentResult(bytes, "application/pdf");
            PDF.FileDownloadName = "入住合同.pdf";
            return PDF;
        }
    }
}