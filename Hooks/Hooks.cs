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
		public static IWebDriver Driver { get; set; }
		public static ScenarioContext _scenarioContext { get; set; }
		public static FeatureContext _featureContext { get; set; }

		public static string basePath = AppDomain.CurrentDomain.BaseDirectory;


		public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext)
		{
			_scenarioContext = scenarioContext;
			_featureContext = featureContext;
		}


		//[BeforeTestRun]
		//public static string BeforeTestRun()
		//{
		//	Environment.CurrentDirectory = $"{basePath}/allure-results"; ;
		//	return Environment.CurrentDirectory;
		//}

		[BeforeScenario]
		public void BeforeScenario()
		{
			Console.WriteLine("***********************************************************************************************************");
			Console.WriteLine("[ Configuration ] - Initializing driver configuration");
			Console.WriteLine($"Feature Name : {_featureContext.FeatureInfo.Title}");
			Console.WriteLine($"Scenario Name : {_scenarioContext.ScenarioInfo.Title}");
			Console.WriteLine($"Base Path : {basePath}");
			Console.WriteLine($"allure base : {Environment.CurrentDirectory}");
			Console.WriteLine("***********************************************************************************************************");
			
			Driver = CreateDriver.initConfig();
		}

		[AfterScenario]
		public void AfterScenario()
		{
			Console.WriteLine("***********************************************************************************************************");
			Console.WriteLine("[ Driver Status ] - Clean and close the intance of the driver");
			Console.WriteLine("***********************************************************************************************************");
			Driver.Quit();
		}
	}
}
