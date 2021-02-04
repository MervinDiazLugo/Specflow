using System;
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

		StepDefinitionsBase stepDefinitionsBase = new StepDefinitionsBase();

		public static IWebDriver GetDriver()
		{
			return Hooks.Hooks.Driver;
		}


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

		[Given(@"Navigates to '(.*)'")]
		public void GivenNavigatesTo(string url)
		{
			GetDriver().Url = $"{url}";
			GetDriver().Manage().Window.Maximize();
			WaitForDOMComplete();
			SwitchToWindowsName("Principal");
		}

		[Given(@"I open new tab with URL '(.*)'")]
		[Then(@"I open new tab with URL '(.*)'")]
		public void GivenIOpenNewTabWithURL(string url)
		{
			IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
			js.ExecuteScript("window.open(arguments[0], '_blank')", url);
			WaitForDOMComplete();
		}

		[Then(@"I go to '(.*)' window")]
		public void ThenIGoToWindow(string window)
		{
			SwitchToWindowsName(window);
		}

		[Given(@"Wait for DOM Complete")]
		public void WhenWaitForDOMComplete()
		{
			WaitForDOMComplete();
		}

		[Then(@"I put '(.*)' as DOM")]
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
			text = ReplaceWithContextValues(text);
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

		[Given(@"I pause '(.*)' seconds")]
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

		[Then(@"I save text of label '(.*)' in scenario context")]
		[Given(@"I save text of label '(.*)' in scenario context")]
		public void ThenISaveTextOfLabelInContext(string element)
		{
			var TestElement = GetElement(element);
			AddKeyValuePairToScenarioContext(element, TestElement.Text);
		}

		[Then(@"I Switch to '(.*)' Iframe")]
		public void ThenISwitchToIframe(string Iframe)
		{
			SwitchToFrame(Iframe);
		}

		[Then(@"I close Alert dialog")]
		[Given(@"I close Alert dialog")]
		public void GivenICloseAlertDialog()
		{
			IsAlertPresent();
		}

		[Then(@"I scroll into element '(.*)'")]
		[Given(@"I scroll into element '(.*)'")]
		public void GivenIScrollIntoElement(string element)
		{
			ScrolltoElement(element);
		}

		[Then(@"I set '(.*)' checkbox to select")]
		[Given(@"I set '(.*)' checkbox to select")]
		public void GivenISetCheckboxToSelect(string element)
		{
			SetCheckboxToSelect(element);
		}

		[Given(@"I set '(.*)' value in Data Scenario")]
		public void GivenISetValueInDataScenario(string value)
		{
			AddKeyValuePairToScenarioContext(value, readProperties(value));
		}




	}
}
