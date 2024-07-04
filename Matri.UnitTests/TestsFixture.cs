using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matri.UnitTests
{
    public class TestsFixture : IDisposable
    {

        private AppiumDriver<AndroidElement> _driver;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TestsFixture()
        {
            var appPath = "com.christianjodi.app";
            Environment.SetEnvironmentVariable(AppiumServiceConstants.NodeBinaryPath, @"C:\Program Files\nodejs\node.exe");
            var driverOption = new AppiumOptions();
            driverOption.AddAdditionalCapability(MobileCapabilityType.PlatformName,"Android");
            driverOption.AddAdditionalCapability("appium:automationName", "UiAutomator2");
            driverOption.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android 28");
            driverOption.AddAdditionalCapability(MobileCapabilityType.App, appPath);

            driverOption.AddAdditionalCapability("chromedriverExecutable", @"E:\Chrome-Download-Folder\chromedriver_win32\chromedriver.exe");

            _driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/"), driverOption);
            
        }
    }
}
