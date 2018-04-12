using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public JsonResult UploadImg(int Size,IFormFile Files)
        {
            string[] FileType = { ".jpg", ".png", ".jpeg", ".bmp", ".gif", ".ico" };
            //文件后缀
            var FileExtension = Path.GetExtension(Files.FileName);
            if(FileExtension==null)
                return new JsonResult(new { data = "", flag = -1, msg = "文件没有后缀名！", HttpCode = 50 });
            if (!FileType.Contains(FileExtension))
                return new JsonResult(new { data = "", flag = -1, msg = "文件类型不正确！", HttpCode = 50 });
            long bytes = Files.Length;
            if(Size!=0)
                if(bytes>1024*1024*Size)
                    return new JsonResult(new { data = "", flag = -1, msg = $"请限制图片大小在{Size}M以内！", HttpCode = 50 });
            if (bytes > 1024 * 1024 * 2) //2M
                return new JsonResult(new { data = "", flag = -1, msg = "请限制图片大小在2M以内！", HttpCode = 50 });
            var webRootPath = Environment.WebRootPath;
            string RootPath = $"/Upload/Images/{DateTime.Now.ToString("yyyyMMdd")}/";
            string SavePath = webRootPath + RootPath;
            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);
            using (FileStream fs = System.IO.File.Create(SavePath + Files.FileName))
            {
                Files.CopyTo(fs);
                fs.Flush();
            }
            return new JsonResult(new { data = RootPath+ Files.FileName, flag = 1, msg = "上传成功！", HttpCode = 10 });
        }
    }
}