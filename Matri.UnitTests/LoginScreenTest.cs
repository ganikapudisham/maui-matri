using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;

namespace Matri.UnitTests
{
    public class LoginScreenTest
    {
        private AppiumDriver<AndroidElement> _driver;

        public LoginScreenTest()
        {
            var appPath = "com.christianjodi.app";
            Environment.SetEnvironmentVariable(AppiumServiceConstants.NodeBinaryPath, @"C:\Program Files\nodejs\node.exe");
            var driverOption = new AppiumOptions();
            driverOption.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOption.AddAdditionalCapability("appium:automationName", AutomationName.AndroidUIAutomator2);
            driverOption.AddAdditionalCapability(MobileCapabilityType.DeviceName, "x86-Pixel 5 - API 28");
            driverOption.AddAdditionalCapability(MobileCapabilityType.App, appPath);

            driverOption.AddAdditionalCapability("chromedriverExecutable", @"C:\Users\ganik\Downloads\chromedriver-win64\chromedriver-win64\chromedriver.exe");

             _driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), driverOption);
            //_driver = new AndroidDriver<AndroidElement>(new Uri("http://172.16.87.36:4723/wd/hub"), driverOption);
        }

        [Fact]
        public void LoginScreenLoad()
        {
            Assert.False(1==1, "true");
        }
    }
}