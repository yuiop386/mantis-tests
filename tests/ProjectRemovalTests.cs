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

            if (app.Api.GetProjectCount(account) == 0)
            {
                ProjectData project = new ProjectData()
                {
                    Name = $"PRT_created {rnd.Next(0, 999)}",
                    Description = "PRT_created",
                };
                app.Api.Create(project, account);    
            }

            List<ProjectData> oldData = app.Api.GetProjectList(account);

            ProjectData projectToRemove = oldData[0];

            app.Api.Remove(projectToRemove, account);

            List<ProjectData> newData = app.Api.GetProjectList(account);

            Assert.AreEqual(oldData.Count - 1, newData.Count);

            oldData.Remove(projectToRemove);
            oldData.Sort();
            newData.Sort();

            Assert.AreEqual(oldData, newData);
        }
    }
}