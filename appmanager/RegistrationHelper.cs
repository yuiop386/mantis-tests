using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
            String url = GetConfirmationUrl(account);
            FillPasswordForm(url, account);
            SubmitPasswordForm();
        }

        private void SubmitPasswordForm()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private void FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("password_confirm")).SendKeys(account.Password);
        }

        private string GetConfirmationUrl(AccountData account)
        {
            string message = manager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }

        private void OpenRegistrationForm()
        {
            driver.FindElement(By.XPath("//a[contains(@href, 'manage_user_create_page.php')]")).Click();
        }

        private void SubmitRegistration()
        {
            driver.FindElement(By.XPath("//input[@type = 'submit']")).Click();
        }

        private void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        private void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost:8080/mantisbt-2.25.2/login_page.php";
        }
    }
}