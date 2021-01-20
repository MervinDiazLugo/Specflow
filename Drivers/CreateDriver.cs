using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;


namespace SpecflowSeleniumUnit.Drivers
{
	class CreateDriver
	{
		public static IWebDriver driver { get; set; }
		public static string browser { get; set; }
		public static string os { get; set; }
        public static IWebDriver initConfig()
        {
            try
            {
                browser = ConfigurationManager.AppSettings["browser"].ToUpperInvariant();

            }
            catch (IOException e)
            {
                Console.WriteLine("initConfig Error", e);
            }

            /******** POM Information ********/
            Console.WriteLine("[ POM Configuration ] Browser: " + browser);

            /****** Load the driver *******/
            driver = CreateNewWebDriver(browser);

            return driver;
        }

        public static IWebDriver CreateNewWebDriver(String browser)
        {


            if (string.Equals(browser, "CHROME", StringComparison.OrdinalIgnoreCase))
            {
                ChromeOptions opts = new ChromeOptions();
                opts.AddArguments("--start-maximized");
                opts.AddArgument("test-type");
                opts.AddArguments("disable-extensions");
                driver = new ChromeDriver(opts);
            }

            return driver;
        }

    }


}
