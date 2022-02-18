using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : TestBase
    {
        public static Random rnd = new Random();

        [Test]
        public void ProjectCreationTest()
        {
            AccountData account = new AccountData("administrator", "root");
            ProjectData project = new ProjectData()
            {
                Name = $"PCT_created {rnd.Next(0, 999)}",
                Description = "PCT_crated",
            };

            app.Login.Login(account);
            app.Menu.OpenProjectMenu();

            List<ProjectData> oldData = app.Project.GetProjectList(account);

            app.Project.Create(project);

            List<ProjectData> newData = app.Project.GetProjectList(account);

            Assert.AreEqual(oldData.Count + 1, newData.Count);

            oldData.Add(project);
            oldData.Sort();
            newData.Sort();

            Assert.AreEqual(oldData, newData);
        }
    }
}