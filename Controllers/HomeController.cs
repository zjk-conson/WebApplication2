using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment hostingEnv;//内置的注入

        private ILogger logger;

        private TestService testService;

        //private readonly ILogger<HomeController> _logger;

        public HomeController(TestService testService, IHostingEnvironment hostingEnv,ILoggerFactory loggerFactory, ILogger<HomeController> logger) {

            this.testService = testService;
            this.hostingEnv = hostingEnv;
            this.logger = loggerFactory.CreateLogger(typeof(HomeController));
            //_logger = logger;
        }
        public IActionResult Index()
        {

            #region 自带logger日志  和  用了Nlog插件（代码不用改，在program.cs中加入配置）

            logger.LogDebug("这是一个调试");
            logger.LogWarning("这是一个警告");
            //为了报错进入MyException这个类
            try
            {

                string i = null;
                i.ToString();
            }
            catch (Exception ex)
            {

                logger.LogError(new EventId(), ex, ex.Message);

            }

            #endregion

            #region 内置注入

            /*
             * //内置注入hosting
            bool isDev = hostingEnv.IsDevelopment();
           
            string contentPath =  hostingEnv.ContentRootPath;
            string wwwPath = hostingEnv.WebRootPath;

            string appSettingPath =  Path.Combine(contentPath, "appsettings.json");
            string siteJsPath = Path.Combine(wwwPath, "js/site.js");


            return Content("isDev="+isDev+contentPath+","+wwwPath+","+appSettingPath+","+siteJsPath);
            */

            #endregion


            #region Session写入

            
            HttpContext.Session.SetString("ddd", "ddd");
            

            #endregion

            return View();

            
        }

        public IActionResult About()
        {
            #region 手动注入

            //TestService tse = (TestService) HttpContext.RequestServices.GetService(typeof(TestService));

            //return Content(tse.Hello()); 


            #endregion

            #region 读取Session

            string um = HttpContext.Session.GetString("ddd");

            return Content("这是" + um);


            #endregion
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
