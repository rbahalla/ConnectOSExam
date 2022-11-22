using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ConnectOSExam
{
    internal class Program
    {
        static IWebDriver driver;
        static void Main(string[] args)
        {
            JObject obj = LoadJSON();
            //Console.WriteLine(obj["baseURL"]);
            var list = StartScraping(obj);
            Console.WriteLine(string.Join(", ", list));
            Console.ReadLine();
        }

        public static JObject LoadJSON() {
            using (StreamReader reader = new StreamReader("./resources/testData.json")) {
                string jsonText = reader.ReadToEnd();
                return  JObject.Parse(jsonText);                
            }
        }

        public static IList<string> StartScraping(JObject obj)
        {
            driver = new ChromeDriver();
            List<string> list = new List<string>();
            try
            {
                //Navigate to the page
                driver.Navigate().GoToUrl(obj["baseURL"].ToString());
                //Find the list using XPath
                IList<IWebElement> children = driver.FindElements(By.XPath(obj["element"].ToString()));
                //Loop through each element and add it to a list
                foreach (IWebElement e in children)
                {
                    list.Add(e.GetAttribute("text"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                driver.Quit();
            }
            return list;            
        }

   
    }
}
