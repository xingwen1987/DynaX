using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.DynaX.Caches.Web.Models;

namespace AspNetCore.DynaX.Caches.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string MemoryCacheProvider = "MemoryCacher";

        public IActionResult Index()
        {
            var cacheDirs = DynaX.Caches.MemoryCaches.Get<Dictionary<string, string>>(MemoryCacheProvider) ?? new Dictionary<string, string>();
            cacheDirs.TryAdd("User-01", "Rich-01");
            cacheDirs.TryAdd("User-02", "Rich-02");
            cacheDirs.TryAdd("User-03", "Rich-03");
            DynaX.Caches.MemoryCaches.Set(MemoryCacheProvider, cacheDirs);
            cacheDirs = DynaX.Caches.MemoryCaches.Get<Dictionary<string, string>>(MemoryCacheProvider);
            return Json(cacheDirs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
