using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.DynaX.UnitOfWorks.Web.DbContexts;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.DynaX.UnitOfWorks.Web.Models;

namespace AspNetCore.DynaX.UnitOfWorks.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TestUow()
        {
            var numStart = 0;

            #region 测试数据库

            var dataSource1 = new DynaX.DataBaseInfo { Type = DynaX.DataBaseType.SqlServer, ConnnectionString = "Data Source=(local);Initial Catalog=DynaXTest01;User Id=sa;Password=123456;" };
            var dataSource2 = new DynaX.DataBaseInfo { Type = DynaX.DataBaseType.SqlServer, DataSource = "(local)", Catalog = "DynaXTest02", UserId = "sa", UserPassword = "123456" };
            var dataSource3 = new DynaX.DataBaseInfo { Type = DynaX.DataBaseType.SqlServer, DataSource = "(local)", Catalog = "DynaXTest03", UserId = "sa", UserPassword = "123456" };
            var dataSource4 = new DynaX.DataBaseInfo { Type = DynaX.DataBaseType.SqlServer, DataSource = "(local)", Catalog = "DynaXTest04", UserId = "sa", UserPassword = "123456" };

            #endregion

            #region WarehouseInfo 的测试数据

            var wmsData = new WarehouseInfo
            {
                Id = Guid.NewGuid(),
                Province = "四川省",
                City = "成都市",
                CreateDateTime = DateTime.Now,
                Recycle = false
            };
            wmsData.Name = "我的仓库-" + wmsData.Id;
            wmsData.Address = "四川省成都市第 “ " + wmsData.Id + " ”号仓库";
            var wmsList = new List<WarehouseInfo>();
            for (int i = 0; i < 10; i++)
            {
                var wmsObj = new WarehouseInfo
                {
                    Id = Guid.NewGuid(),
                    Province = "四川省",
                    City = "成都市",
                    CreateDateTime = DateTime.Now,
                    Recycle = false
                };
                wmsObj.Name = "我的仓库-" + wmsObj.Id;
                wmsObj.Address = "四川省成都市第 “ " + wmsObj.Id + " ”号仓库";
                wmsList.Add(wmsObj);
            }

            #endregion

            #region 返回信息

            var testResult = new StringBuilder();

            #endregion

            #region 数据库 01 测试

            #region TestData1 的测试数据

            var testData1 = new TestData
            {
                Guid = Guid.NewGuid(),
                Number = decimal.Parse("1.23456"),
                DateTime = DateTime.Now,
                Bool = false
            };
            testData1.String = testData1.Guid.ToSafeString();
            var testList1 = new List<TestData>();
            for (int i = 0; i < 10; i++)
            {
                var testObj = new TestData
                {
                    Guid = Guid.NewGuid(),
                    Number = decimal.Parse("1.23456"),
                    DateTime = DateTime.Now,
                    Bool = false
                };
                testObj.String = testObj.Guid.ToSafeString();
                testList1.Add(testObj);
            }

            #endregion

            var dbUnitOfWork = new DynaX.UnitOfWorkX<TestContext>(dataSource1);
            var wmsRepo = dbUnitOfWork.Repository<WarehouseInfo>();
            wmsRepo.Insert(wmsData);
            await wmsRepo.InsertAsync(wmsList);
            var testRepo = dbUnitOfWork.Repository<TestData>();
            await testRepo.InsertAsync(testData1);
            await testRepo.InsertAsync(testList1);
            var result1 = dbUnitOfWork.Commit();
            testResult.AppendLine(result1.ToJson());

            #endregion

            #region 数据库 02 测试

            #region TestData2 的测试数据

            var testData2 = new TestData
            {
                Guid = Guid.NewGuid(),
                Number = decimal.Parse("1.23456"),
                DateTime = DateTime.Now,
                Bool = false
            };
            testData2.String = testData2.Guid.ToSafeString();
            var testList2 = new List<TestData>();
            for (int i = 0; i < 10; i++)
            {
                var testObj = new TestData
                {
                    Guid = Guid.NewGuid(),
                    Number = decimal.Parse("1.23456"),
                    DateTime = DateTime.Now,
                    Bool = false
                };
                testObj.String = testObj.Guid.ToSafeString();
                testList2.Add(testObj);
            }

            #endregion

            dbUnitOfWork = dbUnitOfWork.ChangeDataBase(dataSource2);
            wmsRepo = dbUnitOfWork.Repository<WarehouseInfo>();
            wmsRepo.Insert(wmsData);
            await wmsRepo.InsertAsync(wmsList);
            testRepo = dbUnitOfWork.Repository<TestData>();
            testRepo.Insert(testData2);
            await testRepo.InsertAsync(testList2);
            var result2 = dbUnitOfWork.Commit();
            testResult.AppendLine(result2.ToJson());

            #endregion

            #region 数据库 03 测试

            #region TestData2 的测试数据

            var testData3 = new TestData
            {
                Guid = Guid.NewGuid(),
                Number = decimal.Parse("1.23456"),
                DateTime = DateTime.Now,
                Bool = false
            };
            testData3.String = testData3.Guid.ToSafeString();
            var testList3 = new List<TestData>();
            for (int i = 0; i < 10; i++)
            {
                var testObj = new TestData
                {
                    Guid = Guid.NewGuid(),
                    Number = decimal.Parse("1.23456"),
                    DateTime = DateTime.Now,
                    Bool = false
                };
                testObj.String = testObj.Guid.ToSafeString();
                testList3.Add(testObj);
            }

            #endregion

            dbUnitOfWork = dbUnitOfWork.ChangeDataBase(dataSource3);
            wmsRepo = dbUnitOfWork.Repository<WarehouseInfo>();
            wmsRepo.Insert(wmsData);
            await wmsRepo.BulkInsertAsync(wmsList);
            testRepo = dbUnitOfWork.Repository<TestData>();
            await testRepo.InsertAsync(testData3);
            await testRepo.InsertAsync(testList3);
            var result3 = dbUnitOfWork.Commit();
            testResult.AppendLine(result3.ToJson());

            #endregion

            return Json(testResult.ToSafeString());
        }

        public async Task<IActionResult> TestTypes()
        {
            var obj1 = new
            {
                P1 = "foo",
                P2 = 20,
                P3 = "yo"
            };

            var obj2 = new
            {
                P1 = "bar",
                P4= "SECRET!!",
                P5 = "more stuff"
            };

            var result = DynaX.Utils.Types.Merger.Ignore(() => obj1.P1)
                .Use(() => obj2.P1)
                .Merge(obj1, obj2);

            return Json(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
