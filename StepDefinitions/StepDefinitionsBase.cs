using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace SpecflowSeleniumUnit.StepDefinitions
{
    public class StepDefinitionsBase
    {
        public static IWebDriver driver { get; set; }

        public string jsonString { get; set; }

        public dynamic EntityString { get; set; }

        public string GetFieldBy { get; set; }

        public string ValueToFind { get; set; }

        public static IWebDriver Driver => Hooks.Hooks.Driver;

        public int nWindows { get; set; }

        public int timeOut = 15;

        public StepDefinitionsBase()
        {

        }

        public static void WaitForDOMComplete()
        {

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(40));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            Console.WriteLine("Document.readyState : Complete");
        }

        public string GetFullFilePath(string fileName)
        {
            string fullFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Pages\" + fileName);
            //Console.WriteLine($"DOM: {fullFilePath}");
            return fullFilePath;
        }
        public string ReadJsonContentFromFile(string fileName)
        {
            fileName = this.GetFullFilePath(fileName);
            jsonString = File.ReadAllText(fileName, Encoding.UTF8);
            //Console.WriteLine($"Content: {jsonString}");
            return jsonString;
        }

        public dynamic ReadEntity(String element)
        {
            try
            {
                dynamic entity = JObject.Parse(jsonString);
                EntityString = entity[element];
                Console.WriteLine(EntityString);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                EntityString = null;
            }

            if (EntityString != null) {
                return EntityString;
            }
            else
            {
                Console.WriteLine($"Element {element} is not found");
                return EntityString = null;
            }
            
            
        }


        public IWebElement GetElement(String element)
        {
            dynamic entity = ReadEntity(element);
            IWebElement SeleniumElement = null;

            if (EntityString != null) 
            {
                GetFieldBy = EntityString["GetFieldBy"];
                ValueToFind = EntityString["ValueToFind"];
            }
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOut));
            try
            {
                switch (GetFieldBy.ToLowerInvariant())
                {
                    case "id":
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(ValueToFind)));
                        SeleniumElement = Driver.FindElement(By.Id(ValueToFind));
                        return SeleniumElement;
                    case "xpath":
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(ValueToFind)));
                        SeleniumElement = Driver.FindElement(By.XPath(ValueToFind));
                        return SeleniumElement;
                    case "name":
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name(ValueToFind)));
                        SeleniumElement = Driver.FindElement(By.Name(ValueToFind));
                        return SeleniumElement;
                    case "linktext":
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText(ValueToFind)));
                        SeleniumElement = Driver.FindElement(By.PartialLinkText(ValueToFind));
                        return SeleniumElement;
                    case "cssselector":
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(ValueToFind)));
                        SeleniumElement = Driver.FindElement(By.CssSelector(ValueToFind));
                        return SeleniumElement;
                    default:
                        SeleniumElement = Driver.FindElement(By.XPath("//*[contains(text(), ' " + ValueToFind + " ')]"));
                        return SeleniumElement;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine($"Element {element}: {GetFieldBy}--->{ValueToFind} is not found");
                return SeleniumElement = null;
            }
            
        }

        public SelectElement GetFieldSelect(string element)
        {
            var selectElement = GetElement(element);
            return new SelectElement(selectElement);
        }

        public void TextInElement(string element, string value)
        {
            try
            {
                var SeleniumElement = this.GetElement(element);
                value = ReplaceWithContextValues(value);
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(SeleniumElement));
                IsElementVisible(SeleniumElement);
                Console.WriteLine($"Value of element: {SeleniumElement.Text}");
                Assert.AreEqual(value.ToLower(), SeleniumElement.Text.ToLower());
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine(e + $"{element} is not found");
                throw;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e + $"{element} is TimeOut");
                throw;
            }
        }
        public bool IsElementVisible(IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }

        public bool CheckElementIsVisible(string element)
        {
            bool iselementpresent;
            IWebElement SeleniumElement = this.GetElement(element);
            try
            {
                iselementpresent = IsElementVisible(SeleniumElement);

            }
            catch (Exception ex)
            {
                if (ex is TimeoutException || ex is NoSuchElementException || ex is WebDriverException)
                {
                    Console.WriteLine(ex + $"CheckElementIsVisible: object was not found {element}");
                }
                iselementpresent = false;
            }
            return iselementpresent;
        }

        protected void AddKeyValuePairToScenarioContext(string key, string value)
        {
            if (ScenarioContext.Current.ContainsKey(key) == false)
            {
                ScenarioContext.Current.Add(key, value);
                Console.WriteLine("New key was add to scenario context: " + key + " with the value: " + value);
            }

        }

        public void AddKeyValuePairToFeatureContext(string key, string value)
        {
            if (FeatureContext.Current.ContainsKey(key) == false)
            {
                FeatureContext.Current.Add(key, value);
                Console.WriteLine("New key was add to feature context: " + key + " with the value: " + value);
            }

        }

        public string ReplaceWithContextValues(string text)
        {
            var scenarioMatches = Regex.Matches(text, @"(?<=\{scenario\:)\w+(?=\})").OfType<Match>().Select(m => m.ToString()).Distinct().ToList();
            var featureMatches = Regex.Matches(text, @"(?<=\{feature\:)\w+(?=\})").OfType<Match>().Select(m => m.ToString()).Distinct().ToList();

            foreach (var match in scenarioMatches)
            {
                if (ScenarioContext.Current.Keys.Contains(match.ToString()))
                    text = Regex.Replace(text, @"(\{scenario\:)" + match + @"(\})", ScenarioContext.Current[match.ToString()].ToString());
            }

            foreach (var match in featureMatches)
            {
                if (FeatureContext.Current.Keys.Contains(match.ToString()))
                    text = Regex.Replace(text, @"(\{feature\:)" + match + @"(\})", FeatureContext.Current[match.ToString()].ToString());
            }
            return text;
        }
        
        public void SwitchToWindowsName(string ventana)
        {
            if (ScenarioContext.Current.ContainsKey(ventana) == true)
            {
                Driver.SwitchTo().Window((ScenarioContext.Current[ventana]).ToString());
                Console.WriteLine("volviendo a " + ventana + " : " + ScenarioContext.Current[ventana]);
            }
            else
            {
                nWindows = (Driver.WindowHandles).Count - 1;
                ScenarioContext.Current.Add(ventana, Driver.WindowHandles[nWindows]);
                Console.WriteLine(nWindows);
                Driver.SwitchTo().Window((ScenarioContext.Current[ventana]).ToString());
                Driver.Manage().Window.Maximize();
                Console.WriteLine("Estas en " + ventana + " : " + ScenarioContext.Current[ventana]);
            }
        }


    }

}

