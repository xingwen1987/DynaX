using System.Data;
using AspNetCore.DynaX.Web.Models;
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
    }
}
