namespace SFRegression.Models.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenQA.Selenium;


    public abstract class BasePage
    {
        protected IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver ?? throw new WebDriverException("Webdriver is not initialised");
        }
    }
}
