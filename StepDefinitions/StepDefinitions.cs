﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using SpecflowSeleniumUnit.Hooks;
using System.Threading;

namespace SpecflowSeleniumUnit.StepDefinitions
{
	[Binding]
	public sealed class StepDefinitions : StepDefinitionsBase
	{

		private readonly ScenarioContext _scenarioContext;
		StepDefinitionsBase stepDefinitionsBase = new StepDefinitionsBase();

		public StepDefinitions(ScenarioContext scenarioContext)
		{
			_scenarioContext = scenarioContext;

		}

		public static IWebDriver Driver => Hooks.Hooks.Driver;


		public static string Environment
		{
			get
			{
				return ConfigurationManager.AppSettings["DefaultEnvironment"];
			}
		}

		public static string BaseUrl
		{
			get
			{
				return ConfigurationManager.AppSettings[$"{Environment}:MainAppUrlBase"];
			}
		}

		[Given("the first number is (.*)")]
		public void GivenTheFirstNumberIs(int number)
		{
			//TODO: implement arrange (precondition) logic
			// For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata 
			// To use the multiline text or the table argument of the scenario,
			// additional string/Table parameters can be defined on the step definition
			// method. 

			Console.WriteLine("Primer paso");
		}

		[Given("the second number is (.*)")]
		public void GivenTheSecondNumberIs(int number)
		{
			//TODO: implement arrange (precondition) logic
			// For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata 
			// To use the multiline text or the table argument of the scenario,
			// additional string/Table parameters can be defined on the step definition
			// method. 

			Console.WriteLine("2do paso");
		}

		[When("the two numbers are added")]
		public void WhenTheTwoNumbersAreAdded()
		{
			//TODO: implement act (action) logic

			Console.WriteLine("3er paso");
		}

		[Then("the result should be (.*)")]
		public void ThenTheResultShouldBe(int result)
		{
			//TODO: implement assert (verification) logic

			Console.WriteLine("4to paso");
		}

		[Given(@"I am in main app")]
		public void GivenIAmInMainApp()
		{
			Console.WriteLine($"{BaseUrl}");
			Driver.Url = $"{BaseUrl}";
			Driver.Manage().Window.Maximize();
			StepDefinitionsBase.WaitForDOMComplete();
		}

		[Given(@"Wait for DOM Complete")]
		public void WhenWaitForDOMComplete()
		{
			WaitForDOMComplete();
		}

		[Given(@"I put '(.*)' as DOM")]
		public void ThenIPutAsDOM(string jsonFile)
		{
			ReadJsonContentFromFile(jsonFile);
		}

		[Given(@"I set '(.*)' with '(.*)'")]
		[Then(@"I set '(.*)' with '(.*)'")]
		public void ThenISetWith(string element, string text)
		{
			var TestElement = GetElement(element);
			TestElement.SendKeys(text);
		}

		[Given(@"I click on '(.*)' element")]
		[Then(@"I click on '(.*)' element")]
		public void IClickOnElement(string element)
		{
			var TestElement = GetElement(element);
			TestElement.Click();
		}

		[Given(@"I select '(.*)' dropdown by text '(.*)'")]
		[Then(@"I select '(.*)' dropdown by text '(.*)'")]
		public void ThenISelectDropdownByText(string element, string text)
		{
			var TestElement = GetFieldSelect(element);
			TestElement.SelectByText(text);
		}

		[When(@"I pause '(.*)' seconds")]
		public void WhenIPauseSeconds(int pause)
		{
			pause = pause * 1000;
			Thread.Sleep(pause);
		}


		[Then(@"I check element '(.*)' contains '(.*)'")]
		[Given(@"I check element '(.*)' contains '(.*)'")]
		public void GivenICheckElementContains(string element, string value)
		{
			TextInElement(element, value);
		}



	}
}
