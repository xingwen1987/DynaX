using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.DynaX.Logs.Web.Models;

namespace AspNetCore.DynaX.Logs.Web.Controllers
{
    public class HomeController : Controller
    {
        private static Serilog.ILogger _db01Log;
        private static Serilog.ILogger _db02Log;
        private static Serilog.ILogger _file01Log;
        private static Serilog.ILogger _file02Log;

        public HomeController()
        {
            _db01Log = DynaX.Logs.Serilogs.GetLogger("db01");
            _db02Log = DynaX.Logs.Serilogs.GetLogger("db02");
            _file01Log = DynaX.Logs.Serilogs.GetLogger("log01");
            _file02Log = DynaX.Logs.Serilogs.GetLogger("log02");
        }

        public IActionResult Index(int user = 2, long process = 10000)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Parallel.For(0, user, i => { SerilogTest(process); });
            stopwatch.Stop();
            return Content($"Program Running TIme is：{stopwatch.ElapsedMilliseconds / 1000} 秒。");
        }

        private static void SerilogTest(long process)
        {
            var maxDegreeOfParallelism = Environment.ProcessorCount * 10;
            Parallel.For(0, process, new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism }, i =>
            {
                _file01Log.Debug("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志01：Debug！");
                _file01Log.Information("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志01：Information！");
                _file01Log.Warning("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志01：Warning！");
                _file01Log.Error("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志01：Error！");
                _file01Log.Fatal("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志01：Fatal！");
                _file01Log.Verbose("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志01：Verbose！");

                _file02Log.Debug("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志02：Debug！");
                _file02Log.Information("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志02：Information！");
                _file02Log.Warning("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志02：Warning！");
                _file02Log.Error("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志02：Error！");
                _file02Log.Fatal("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志02：Fatal！");
                _file02Log.Verbose("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入日志02：Verbose！");

                _db01Log.Debug("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库01：Debug！");
                _db01Log.Information("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库01：Information！");
                _db01Log.Warning("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库01：Warning！");
                _db01Log.Error("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库01：Error！");
                _db01Log.Fatal("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库01：Fatal！");
                _db01Log.Verbose("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库01：Verbose！");

                _db02Log.Debug("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库02：Debug！");
                _db02Log.Information("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库02：Information！");
                _db02Log.Warning("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库02：Warning！");
                _db02Log.Error("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库02：Error！");
                _db02Log.Fatal("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库02：Fatal！");
                _db02Log.Verbose("ThreadId：" + Thread.CurrentThread.ManagedThreadId + "，写入数据库02：Verbose！");
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
