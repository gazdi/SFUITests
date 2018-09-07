namespace SFRegression.Models.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class SFPage : BasePage
    {
        public By SearchBar { get; private set; }
        public IWebElement SearchField
        {
            get
            {
                return _driver.FindElement(SearchBar);
            }
            private set {}
        }

        public By UtilityBar { get; private set;}
        public IWebElement UtilityBarElement
        {
            get
            {
                return _driver.FindElement(UtilityBar);
            }
            private set {}
        }

        public By ContextBar { get; private set; }
        public IWebElement ContextBarElement
        {
            get
            {
                return _driver.FindElement(ContextBar);
            }
            private set {}
        }

        public By ActiveUtilityPanel { get; private set; }
        public IWebElement ActiveUtilityPanelElement
        {
            get
            {
                return _driver.FindElement(ActiveUtilityPanel);
            }
            private set {}
        }

        public SFPage(IWebDriver driver) : base(driver)
        {
            SearchBar = By.
            UtilityBar = By.ClassName("utilitybar");
            ContextBar = By.ClassName("slds-context-bar");
            ActiveUtilityPanel = By.CssSelector("div[class=slds-utility-panel][class=DOCKED]");
        }
    }
}
