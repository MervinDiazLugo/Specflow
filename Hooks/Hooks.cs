using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using SpecflowSeleniumUnit.Drivers;

namespace SpecflowSeleniumUnit.Hooks
{
	[Binding]
	public sealed class Hooks
	{
		public static IWebDriver driver { get; set; }

		[BeforeScenario]
		public void BeforeScenario()
		{
			Console.WriteLine("***********************************************************************************************************");
			Console.WriteLine("[ Configuration ] - Initializing driver configuration");
			Console.WriteLine("***********************************************************************************************************");
			driver = CreateDriver.initConfig();
		}

		[AfterScenario]
		public void AfterScenario()
		{
			Console.WriteLine("***********************************************************************************************************");
			Console.WriteLine("[ Driver Status ] - Clean and close the intance of the driver");
			Console.WriteLine("***********************************************************************************************************");
			driver.Close();
		}
	}
}
