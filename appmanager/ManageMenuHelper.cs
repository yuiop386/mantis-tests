using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ManageMenuHelper : HelperBase
    {
        public ManageMenuHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void OpenProjectMenu()
        {
            driver.FindElement(By.XPath("//a[@href= '/mantisbt-2.25.2/manage_overview_page.php']")).Click();
            driver.FindElement(By.XPath("//a[@href= '/mantisbt-2.25.2/manage_proj_page.php']")).Click();
        }
    }
}