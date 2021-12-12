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
        public string LastProjectName { get; set; }
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
            LastProjectName = driver.FindElement(By.XPath(".//*[@id='dm-react-shared-container']/div/div[9]/header/div[1]/div[2]/div[2]/div/div/button/span[1]")).Text;
            var rows = driver.FindElements(By.XPath(".//div[@class='wqxd5v-0 fvjxLo DocumentNameCell__data-text']"));

            //Сбор информации
            List<string> namesList = new List<string>();
            for (int i = 0; i < rows.Count; i++)
            {
                IWebElement row = rows[i];
                if (extensions.Any(s => row.Text.EndsWith(s)))
                {
                    namesList.Add(row.Text);
                }
            }
            namesList.Sort();
            return namesList;
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
