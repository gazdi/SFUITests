using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSFDemo
{
    public class VeryFirstSFTest
    {
        private IWebDriver _driver;

        [OneTimeSetUp]
        public void Setup_Driver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();

        }

        [OneTimeTearDown]
        public void Teardown_Driver()
        {
            //_driver.Quit();
        }

        [Test]
        public void UATGotoLoginpage()
        {
            //Arrange
            //-

            //Act
            _driver.Navigate().GoToUrl("http://retailmerchantservices--uat.lightning.force.com");

            //Assert
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual("Login | Salesforce", _driver.Title);
        }

        [Test]
        public void UATLogin()
        {
            //Arrange
            //this test assumes that the login page is already displayed

            //Act
            IWebElement _usernameField = _driver.FindElement(By.Id("username"));
            IWebElement _passwordField = _driver.FindElement(By.Id("password"));
            IWebElement _loginButton = _driver.FindElement(By.Id("Login"));

            //Safety check
            Assert.AreEqual("Log In to Sandbox", _loginButton.GetAttribute("Value"));

            _usernameField.Clear();
            _usernameField.SendKeys("testbda4@retailmerchantservices.co.uk.uat");
            _passwordField.Clear();
            _passwordField.SendKeys("fourfourfour4");
            _loginButton.Click();
            System.Threading.Thread.Sleep(7000);

            //Assert
            Assert.AreEqual("Sandbox: UAT", _driver.FindElement(By.ClassName("level-info")).Text);
        }

        [Test]
        public void Logout()
        {
            //Arrange
            //this test assumes that the user is logged on

            //Act
            _driver.FindElement(By.ClassName("oneUserProfileCardTrigger")).Click();
            System.Threading.Thread.Sleep(2000);
            _driver.FindElement(By.LinkText("Log Out")).Click();

            //Assert
            System.Threading.Thread.Sleep(5000);
            Assert.AreEqual("Login | Salesforce", _driver.Title);
        }

        [Test]
        public void CloseAllTabs()
        {
            //Arrange
            //this test assumes that the user is logged on

            //Act
            IEnumerable<IWebElement> _closeButtons = _driver.FindElements(By.ClassName("slds-button--icon-x-small"))
                                                                 .Where(e => e.GetAttribute("title").StartsWith("Close "));
            foreach (IWebElement btn in _closeButtons)
                btn.Click();

            //Assert
            System.Threading.Thread.Sleep(1000);
            _closeButtons = _driver.FindElements(By.ClassName("slds-button--icon-x-small"))
                                   .Where(e => e.GetAttribute("title").StartsWith("Close "));
            Assert.AreEqual(0, _closeButtons.Count());
        }

        [Test]
        public IWebElement SearchForLead(string FirstName, string LastName, string CompanyName=null)
        {
            //Arrange
            //this test assumes that the user is in the Call Centre Console app
            IWebElement hit = null;

            //Act
            _driver.FindElement(By.ClassName("uiInputTextForAutocomplete")).SendKeys(FirstName + " " + LastName);
            System.Threading.Thread.Sleep(1000);
            _driver.FindElement(By.ClassName("uiInputTextForAutocomplete")).SendKeys(Keys.Return);
            System.Threading.Thread.Sleep(5000);

            ReadOnlyCollection<IWebElement> hitRows = _driver.FindElements(By.TagName("tr"));
            //Could be done in a lambda operator, it is possibly easier to read this way
            foreach (IWebElement row in hitRows)
            {
                String[] celltexts = row.Text.Split('\n');
                if (   celltexts.Length>2
                    && celltexts[0].Equals(FirstName + " " + LastName + "\r")
                    && celltexts[1].Equals(CompanyName + "\r"))
                {
                    hit = row;
                    break;
                }
            }
            return hit;
        }

        [Test]
        public void Goto_Call_Centre_Console()
        {
            //Arrange
            //this test assumes that the user is logged in

            //Act
            if ("Call Centre Console".Equals(_driver.FindElement(By.ClassName("appName"))
                                                    .FindElement(By.ClassName("label")).Text))
                return;

            _driver.FindElement(By.ClassName("salesforceIdentityAppLauncherHeader")).Click();
            System.Threading.Thread.Sleep(2000);
            ReadOnlyCollection<IWebElement> appTitles = _driver.FindElements(By.ClassName("appTileTitle"));
            appTitles.First(app => "Call Centre Console" == app.GetAttribute("title")).Click();
            System.Threading.Thread.Sleep(5000);

            //Assert
            Assert.AreEqual("Call Centre Console", _driver.FindElement(By.ClassName("appName"))
                                                          .FindElement(By.ClassName("label")).Text);
        }

        [Test]
        [TestCase("0999123499", "AutoTest Company 99", "Auto99", "Test99", "csaba.bobak@retailmerchantservices.co.uk", "PC9 9ZZ", "created by an automated test")]
        public void Create_Lead(string PhoneNumber, string CompanyName, string FirstName, string LastName, string EmailAddress, string PostCode, string Notes)
        {
            //Arrange
            //this test assumes that the user is in the Call Centre Console app

            //Act
            //_driver.FindElements(By.ClassName("slds-utility-bar__action")).First(btn => btn.GetAttribute(""))
            _driver.FindElements(By.ClassName("itemTitle")).First(btn => btn.Text.Equals("New Prospect")).Click();
            System.Threading.Thread.Sleep(1000);
            _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElement(By.ClassName("uiInputText")).SendKeys(PhoneNumber);
            _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElement(By.ClassName("slds-button--brand")).Click();
            IWebElement ok = null;
            while (ok == null)
            {
                System.Threading.Thread.Sleep(4000);
                _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElement(By.ClassName("slds-button--brand")).Click();
                System.Threading.Thread.Sleep(1000);
                try
                {
                    ok = _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElement(By.XPath("//div[contains(text(), 'Ok, Great.')]"));
                }
                catch (NoSuchElementException)
                {
                    ok = null;
                }
            }

            _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElements(By.ClassName("uiInput--input"))[0].SendKeys(CompanyName);
            _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElements(By.ClassName("uiInput--input"))[1].SendKeys(FirstName);
            _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElements(By.ClassName("uiInput--input"))[2].SendKeys(LastName);
            _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElements(By.ClassName("uiInput--input"))[3].SendKeys(EmailAddress);
            _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElements(By.ClassName("uiInput--input"))[4].SendKeys(PostCode);
            _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElement(By.ClassName("uiInput--textarea")).SendKeys(Notes);
            _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElement(By.ClassName("slds-button--brand")).Click();
            System.Threading.Thread.Sleep(2000);

            //Assert
            ok = null;
            try
            {
                ok = _driver.FindElement(By.ClassName("flowruntimeFlowRuntimeForFlexiPage")).FindElement(By.ClassName("slds-button--brand"));
            }
            catch (NoSuchElementException)
            {
                ok = null;
            }
            Assert.NotNull(ok);

            ok.Click();
            _driver.FindElements(By.ClassName("itemTitle")).First(btn => btn.Text.Equals("New Prospect")).Click();
        }

        [Test]
        [TestCase("Auto99", "Test99", "AutoTest Company 99")]
        public void Open_Lead(string FirstName, string LastName, string CompanyName)
        {
            //Arrange
            //this test assumes that the user is logged in and is running the Call Centre Console app

            //Act
            IWebElement _leadRow = SearchForLead(FirstName, LastName, CompanyName);
            Assert.NotNull(_leadRow);
            _leadRow.FindElement(By.ClassName("outputLookupLink")).Click();

            System.Threading.Thread.Sleep(3000);

            //Assert
            Assert.AreEqual(FirstName + " " + LastName,
                            _driver.FindElement(By.ClassName("primaryFieldRow"))
                                   .FindElement(By.TagName("h1"))
                                   .FindElement(By.TagName("span"))
                                   .Text);
            //ugly way to check whether we are on the right lead page
            Assert.GreaterOrEqual(_driver.FindElements(By.XPath("//*[contains(text(),'" + CompanyName + "')]")).Count, 3);
        }

        [Test]
        [Category("Regression")]
        [TestCase("0999123499", "AutoTest Company 99", "Auto99", "Test99", "csaba.bobak@retailmerchantservices.co.uk", "PC9 9ZZ", "created by an automated test")]
        public void UAT_Create_and_Open_Lead(string PhoneNumber, string CompanyName, string FirstName, string LastName, string EmailAddress, string PostCode, string Notes)
        {
            //Arrange
            UATGotoLoginpage();
            UATLogin();
            Goto_Call_Centre_Console();

            //Act
            Create_Lead(PhoneNumber, CompanyName, FirstName, LastName, EmailAddress, PostCode, Notes);
            System.Threading.Thread.Sleep(15000);
            Open_Lead(FirstName, LastName, CompanyName);

            //Asserts are included in Fragments and Atoms

            CloseAllTabs();
            Logout();
        }

        [Test]
        [Category("Regression")]
        public void UAT_Login_Logout()
        {
            UATGotoLoginpage();
            UATLogin();
            Logout();
        }

    }
}