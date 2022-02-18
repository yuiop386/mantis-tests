using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }

            Type(By.Id("username"), account.Name);
            driver.FindElement(By.XPath("//input[@type= 'submit']")).Click();

            Type(By.Id("password"), account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.ClassName("user-info")).Click();
                driver.FindElement(By.CssSelector("#navbar-container > div.navbar-buttons.navbar-header.navbar-collapse.collapse > ul > li.grey.open > ul > li:nth-child(4) > a")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            return !IsElementPresent(By.Id("username"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggedUserName() == account.Name;
        }

        private string GetLoggedUserName()
        {
            return driver.FindElement(By.ClassName("user-info")).Text;
        }
    }
}