using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NameParsing.ParsingData
{
    class BIM360Data
    {
        public string UserEmail { get; }
        public string UserPassword { get; }
        public int UserEthrnSensiv { get; }

        private readonly IWebDriver driver;
        
        public BIM360Data(string email, string password, string url, int sensivityEhternet)
        {
            UserEmail = email;
            UserPassword = password;
            UserEthrnSensiv = sensivityEhternet;
            
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService cService = ChromeDriverService.CreateDefaultService();
            cService.HideCommandPromptWindow = true;
            options.AddArguments("start-maximized");
            options.AddArguments("disable-infobars");
            options.AddArguments("headless");
            driver = new ChromeDriver(cService, options);
            driver.Navigate().GoToUrl(url);
            
            Thread.Sleep(sensivityEhternet);
        }
        
        public List<string> Names(List<string> extensions)
        {
            // Парсинг сайта
            WebOnBtnClick(driver, ".//div[@class='normal-login-block']/button[@id='sign_in']", UserEthrnSensiv);
            driver.SwitchTo().Frame(this.driver.FindElement(By.XPath(".//iframe[@id='loginframe']")));
            WebOnTextInput(driver, ".//*[@id='userName']", UserEmail);
            WebOnBtnClick(driver, ".//*[@id='verify_user_btn']/span", UserEthrnSensiv);
            WebOnTextInput(driver, ".//*[@id='password']", UserPassword);
            WebOnBtnClick(driver, ".//*[@id='btnSubmit']", UserEthrnSensiv * UserEthrnSensiv/100);
            // Старая версия - вывод строк
            //var rows = driver.FindElements(By.XPath(".//div[@class='wqxd5v-0 fvjxLo DocumentNameCell__data-text']"));
            var rows = driver.FindElements(By.XPath(".//div[@class='MatrixTable__table MatrixTable__table-frozen-left']/div/div"));
            var rows1 = driver.FindElements(By.XPath(".//div[@class='MatrixTable__table MatrixTable__table-frozen-left']"))[0].GetAttribute("height");

            string[] rowsStream = rows[0].Text.Split("\r\n");


            //Сбор информации
            List<string> namesList = new List<string>();
            foreach (string str in rowsStream)
            {
                var t = str;
                if (extensions.Any(s => str.EndsWith(s)))
                {
                    namesList.Add(str);
                }
            }
            namesList.Sort();
            return namesList;
            
            // Старая версия - вывод строк
            /*
            for (int i = 0; i < rows.Count; i++)
            {
                IWebElement row = rows[i];
                var t = row.Text;
                if (extensions.Any(s => row.Text.EndsWith(s)))
                {
                    namesList.Add(row.Text);
                }
            }
            */
        }
        
        private static void WebOnBtnClick(IWebDriver drv, string xpath, int sensivity)
        {
            IWebElement btn = drv.FindElement(By.XPath(xpath));
            btn.Click();
            Thread.Sleep(sensivity);
        }
        
        private static void WebOnTextInput(IWebDriver drv, string xpath, string inputData)
        {
            IWebElement btn = drv.FindElement(By.XPath(xpath));
            btn.SendKeys(inputData);
        }
    }
}
