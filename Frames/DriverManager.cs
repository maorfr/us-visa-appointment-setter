using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace Frames
{
    public static class DriverManager
    {
        internal static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    _driver = new ChromeDriver();
                    _driver.Manage().Window.Maximize();
                }
                return _driver;
            }

        }
        private static IWebDriver _driver;

        public static void Start()
        {
            //KillChrome();
        }

        private static void Stop()
        {
            Driver.Quit();
            _driver = null;
        }

        private static void KillChrome()
        {
            var victims = from p in Process.GetProcesses()
                          where p.ProcessName.Contains("chrome")
                          select p;
            foreach (var process in victims)
            {
                process.Kill();
            }
        }
    }
}
