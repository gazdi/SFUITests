namespace SFRegression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenQA.Selenium;
    

    public static class Helper
    {
        internal static void HighlightElement(IWebDriver driver, IWebElement element)
        {
            var JS = (IJavaScriptExecutor)driver;
            for (var i = 3; i > 0; i--)
            {
                JS.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, " border: 2px solid red;");
                System.Threading.Thread.Sleep(100);
                JS.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "");
                if (i>1) System.Threading.Thread.Sleep(200);
            }
        }
    }
}
