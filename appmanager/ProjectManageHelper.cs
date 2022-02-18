using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ProjectManageHelper : HelperBase
    {
        public ProjectManageHelper(ApplicationManager manager) : base(manager) { }

        private List<ProjectData> projectCache = null;

        public void Create(ProjectData project)
        {
            manager.Menu.OpenProjectMenu();
            InitProjectCreation();
            FillProjectData(project);
            SubmitProjectCreation();
        }

        private void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value = 'Add Project']")).Click();
            projectCache = null;
        }

        private void FillProjectData(ProjectData project)
        {
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
            driver.FindElement(By.Id("project-description")).SendKeys(project.Description);
        }

        private void InitProjectCreation()
        {
            driver.FindElement(By.CssSelector("form[action=\"manage_proj_create_page.php\"]")).Click();
        }

        public void Remove()
        {
            manager.Menu.OpenProjectMenu();
            OpenProject();
            InitProjectRemoval();
            SubmitProjectRemoval();

        }

        private void SubmitProjectRemoval()
        {
            driver.FindElement(By.XPath("//input[@value = 'Delete Project']")).Click();
            projectCache = null;
        }

        private void InitProjectRemoval()
        {
            driver.FindElement(By.CssSelector("form[action=\"manage_proj_delete.php\"]")).Click();
        }

        private void OpenProject()
        {
            driver.FindElement(By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']/tbody/tr/td/a")).Click();
        }

        public List<ProjectData> GetProjectList()
        {
            if (projectCache == null)
            {
                manager.Menu.OpenProjectMenu();
                projectCache = new List<ProjectData>();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']/tbody/tr/td/a"));

                foreach (var element in elements)
                {
                    projectCache.Add(new ProjectData()
                    {
                        Name = element.Text,
                    });
                }
            }

            return new List<ProjectData>(projectCache);
        }

        public int GetProjectCount()
        {
            return driver.FindElements(By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']/tbody/tr/td/a")).Count;
        }
    }
}