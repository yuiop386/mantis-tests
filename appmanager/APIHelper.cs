using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager)
        {
        }

        private List<ProjectData> apiCache = null;

        public List<ProjectData> GetProjectList(AccountData account)
        {
            if (apiCache == null)
            {
                apiCache = new List<ProjectData>();
                Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
                Mantis.ProjectData[] projectData = client.mc_projects_get_user_accessible(account.Name, account.Password);
                foreach (var project in projectData)
                {
                    apiCache.Add(new ProjectData()
                    {
                        Id = project.id,
                        Description = project.description,
                        Name = project.name
                    });
                }
            }
            return new List<ProjectData>(apiCache);
        }

        public int GetProjectCount(AccountData account)
        {
            return GetProjectList(account).Count;
        }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue);
            apiCache = null;
        }

        public void Create(ProjectData projectData, AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.id = projectData.Id;
            project.description = projectData.Description;
            project.name = projectData.Name;
            client.mc_project_add(account.Name, account.Password, project);
            apiCache = null;
        }

        public void Remove(ProjectData projectData, AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.id = projectData.Id;
            project.description = projectData.Description;
            project.name = projectData.Name;
            client.mc_project_delete(account.Name, account.Password, project.id);
            apiCache = null;
        }
    }
}
