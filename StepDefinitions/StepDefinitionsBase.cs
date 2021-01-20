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

namespace SpecflowSeleniumUnit.StepDefinitions
{
    public class StepDefinitionsBase
    {
        public static IWebDriver driver { get; set; }

        public string jsonString { get; set; }

        public dynamic EntityString { get; set; }

        public string GetFieldBy { get; set; }

        public string ValueToFind { get; set; }

        public static IWebDriver Driver => Hooks.Hooks.driver;


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
            try
            {
                switch (GetFieldBy.ToLowerInvariant())
                {
                    case "id":
                        SeleniumElement = Driver.FindElement(By.Id(ValueToFind));
                        return SeleniumElement;
                    case "xpath":
                        SeleniumElement = Driver.FindElement(By.XPath(ValueToFind));
                        return SeleniumElement;
                    case "name":
                        SeleniumElement = Driver.FindElement(By.Name(ValueToFind));
                        return SeleniumElement;
                    case "linktext":
                        SeleniumElement = Driver.FindElement(By.PartialLinkText(ValueToFind));
                        return SeleniumElement;
                    case "cssselector":
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

    }

}

