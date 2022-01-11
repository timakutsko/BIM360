using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            WebOnBtnClick(driver, ".//*[@id='btnSubmit']", UserEthrnSensiv * UserEthrnSensiv / 125);
            var body = driver.FindElements(By.XPath(".//div[@class='MatrixTable__table MatrixTable__table-frozen-left']"));
            var rows = driver.FindElements(By.XPath(".//div[@class='MatrixTable__table MatrixTable__table-frozen-left']/div/div"));
            int rowsHeight = rows[0].Size.Height;
            int rowHeight = rows[1].Size.Height;
            int numbItems = rowsHeight / rowHeight;
            int bodyHeight = body[0].Size.Height;
            int numItemsOnWind = bodyHeight / rowHeight > numbItems ? numbItems : bodyHeight / rowHeight;
            int numbScrolling = numbItems % numItemsOnWind == 0 ? numbItems / numItemsOnWind : numbItems / numItemsOnWind + 2;

            //Сбор информации и скроллинг страницы
            List<string> namesList = new List<string>();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            int step = 1;
            do
            {
                string[] rowsStream = rows[0].Text.Split("\r\n");
                foreach (string str in rowsStream)
                {
                    if (extensions.Any(s => str.EndsWith(s)) && !namesList.Contains(str))
                    {
                        namesList.Add(str);
                    }
                }
                if(numbScrolling > 1)
                {
                    IReadOnlyCollection<IWebElement> row = driver.FindElements(By.XPath($".//div[@class='MatrixTable__table MatrixTable__table-frozen-left']/div/div/div[{numItemsOnWind + 1}]"));
                    var b = row.First().Text;
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", row.First());
                    rows = driver.FindElements(By.XPath(".//div[@class='MatrixTable__table MatrixTable__table-frozen-left']/div/div"));
                }
                step++;
            }
            while (step <= numbScrolling);
            driver.Quit();
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
