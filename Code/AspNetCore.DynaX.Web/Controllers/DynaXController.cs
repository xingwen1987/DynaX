using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using AspNetCore.DynaX.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AspNetCore.DynaX.Web.Controllers
{
    [Route("DynaX")]
    public class DynaXController : Controller
    {
        [Route("Extensions/AutoMapper")]
        public IActionResult ExtensionsAutoMapper()
        {
            var table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("first_name", typeof(string));
            table.Columns.Add("last_name", typeof(string));
            table.Rows.Add(100, "Jeff", "Barnes");
            table.Rows.Add(101, "George", "Costanza");
            table.Rows.Add(102, "Stewie", "Griffin");
            table.Rows.Add(103, "Stan", "Marsh");
            table.Rows.Add(104, "Eric", "Cartman");

            var result = table.MapToList<Person>();

            table.Rows.Add(105, "Richfiter", "Xing");
            var result2 = table.MapToList<Person>();

            var jsonArr = new JArray { JToken.FromObject(result), JToken.FromObject(result2) };

            return Json(jsonArr);
        }

        [Route("Utils/TypeMerger")]
        public IActionResult UtilsTypeMerger()
        {
            var obj1 = new { P1 = "foo", P2 = 20, P3 = "yo" };
            var obj2 = new { P1 = "bar", P4 = "SECRET!!", P5 = "more stuff" };
            var result = DynaX.Utils.Types.Merger.Ignore(() => obj1.P1).Use(() => obj2.P1).Merge(obj1, obj2);
            return Json(result);
        }

        [Route("Extensions/Dir")]
        public IActionResult TestDir()
        {
            dynamic type = new Program().GetType();
            dynamic host = typeof(IHostingEnvironment);
            var sysDir = Path.GetDirectoryName(type.Assembly.Location);
            var hostDir = Path.GetDirectoryName(host.Assembly.Location);
            var dynaWebDir = DynaX.Web.Path.WebRootPath;
            var dynaContentDir = DynaX.Web.Path.ContentRootPath;
            var staticDir = Directory.GetCurrentDirectory();
            var dirResult = $"Sys目录地址为：{sysDir}；\r\nHost目录地址为：{hostDir}；\r\n系统目录地址为：{staticDir}；\r\nDynaX Web 目录地址为：{dynaWebDir}；\r\nDynaX Web 目录地址为：{dynaContentDir}。";
            return Content(dirResult);
        }

        [HttpPost]
        [Route("Extensions/UploadFile")]
        public async Task<IActionResult> UploadFiles()
        {
            var files = Request.Form.Files;
            if (files == null) throw new NoNullAllowedException("上传文件不能为空。");
            var webRootPath = DynaX.Web.Path.WebRootPath;
            var contentRootPath = DynaX.Web.Path.ContentRootPath;
            foreach (var formFile in files)
            {
                if (formFile.Length < 1) continue;
                var fileExt = formFile.FileName;
                var newFileName = Guid.NewGuid() + fileExt;
                var fileByte = formFile.ToFileBytes(20);

                var saveDir = string.Empty;
                if (fileByte.IsImage()) { saveDir = "images"; }
                if (fileByte.IsOffice()) { saveDir = "documents"; }
                if (saveDir == string.Empty) throw new InvalidDataException("无效的上传文件信息。");
                var filePath = webRootPath + Path.DirectorySeparatorChar + "upload" + Path.DirectorySeparatorChar + saveDir + Path.DirectorySeparatorChar + newFileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            return Ok();
        }
    }
}
