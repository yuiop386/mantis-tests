using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : TestBase
    {
        public static Random rnd = new Random();

        [Test]
        public void ProjectRemovalTest()
        {
            AccountData account = new AccountData("administrator", "root");

            app.Login.Login(account);
            app.Menu.OpenProjectMenu();

            if (app.Project.GetProjectCount(account) == 0)
            {
                ProjectData project = new ProjectData()
                {
                    Name = $"PRT_created {rnd.Next(0, 999)}",
                    Description = "PRT_created",
                };
                app.Api.Create(project, account);
            }

            //to compare api vs browser
            app.Driver.Url = "http://localhost:8080/mantisbt-2.25.2/manage_overview_page.php";
            app.Driver.Url = "http://localhost:8080/mantisbt-2.25.2/manage_proj_page.php";

            List<ProjectData> oldData = app.Project.GetProjectList(account);

            //to compare api vs browser
            int count = app.Project.GetProjectCount_old(account);

            ProjectData projectToRemove = oldData[0];

            app.Api.Remove(projectToRemove, account);

            //to compare api vs browser
            app.Driver.Url = "http://localhost:8080/mantisbt-2.25.2/manage_overview_page.php";
            app.Driver.Url = "http://localhost:8080/mantisbt-2.25.2/manage_proj_page.php";

            List<ProjectData> newData = app.Project.GetProjectList(account);

            //to compare api vs browser
            int newCount = app.Project.GetProjectCount_old(account);

            Assert.AreEqual(oldData.Count - 1, newData.Count);

            oldData.Remove(projectToRemove);
            oldData.Sort();
            newData.Sort();

            Assert.AreEqual(oldData, newData);
        }
    }
}