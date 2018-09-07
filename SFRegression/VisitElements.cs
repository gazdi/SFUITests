namespace SFRegression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using SFRegression.Models.Pages;

    [TestFixture]
    class VisitElements
    {
        private IWebDriver _driver;

        [OneTimeSetUp]
        public void Initialise()
        {
            _driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void Quit()
        {
            _driver.Quit();
        }

        [Test]
        [Category("Playing around)")]
        public void VisitUtilities()
        {
            _driver.Navigate().GoToUrl("http://retailmerchantservices--uat.lightning.force.com");
            var LoginPage = new LoginPage(_driver);

            LoginPage.UsernameField.SendKeys("testbda4@retailmerchantservices.co.uk.uat");
            LoginPage.PasswordField.SendKeys("fourfourfour4");
            LoginPage.LoginButton.Click();

            System.Threading.Thread.Sleep(5000);

            var anyPage = new SFPage(_driver);

            foreach (IWebElement e in _driver.FindElement(anyPage.UtilityBar).FindElements(By.TagName("li")))
            {
                Helper.HighlightElement(_driver, e);
                e.Click();
                System.Threading.Thread.Sleep(1000);
                e.Click();
            }
        }

    }
}
