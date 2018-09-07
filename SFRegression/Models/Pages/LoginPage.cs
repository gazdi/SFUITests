namespace SFRegression.Models.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenQA.Selenium;

    class LoginPage:BasePage
    {
        public static By Username = By.Id("username");
        public IWebElement UsernameField
        {
            get
            {
                return _driver.FindElement(Username);
            }
            private set { }
        }

        public static By Password = By.Id("password");
        public IWebElement PasswordField
        {
            get
            {
                return _driver.FindElement(Password);
            }
            private set { }
        }

        public static By Login = By.Id("Login");
        public IWebElement LoginButton
        {
            get
            {
                return _driver.FindElement(Login);
            }
        }

        public LoginPage(IWebDriver driver) : base(driver) { }
    }
}
