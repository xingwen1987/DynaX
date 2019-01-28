using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AspNetCore.DynaX.DbContexts.Web.DbContexts;
using AspNetCore.DynaX.DbContexts.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.DynaX.DbContexts.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var connId = DynaX.Utils.SeqGuid.Next().ToString();
            var dbContext = await DynaX.DbContexts.CreateDbContext<WarehouseContext>(DynaX.DataBaseType.SqlServer, connId);

            var wareHouseInfo = new WarehouseInfo
            {
                Id = DynaX.Utils.SeqGuid.Next(),
                Name = "我的仓库-" + connId,
                Province = "四川省",
                City = "成都市",
                CreateDateTime = DateTime.Now,
                Recycle = false
            };
            wareHouseInfo.Address = "四川省成都市第 “ " + wareHouseInfo.Id + " ”号仓库";

            using (var dbTrans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    await dbContext.WarehouseInfos.AddAsync(wareHouseInfo);
                    await dbContext.SaveChangesAsync();
                    dbTrans.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            var list = await dbContext.Set<WarehouseInfo>().ToListAsync();
            return Json(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
