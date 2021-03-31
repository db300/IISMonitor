using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;

namespace iHawkIISLibrary
{
    /// <summary>
    /// 网站管理器
    /// </summary>
    public class WebsitesManager : IDisposable
    {
        #region constructor

        public WebsitesManager()
        {
            _serverManager = new ServerManager();
        }

        public void Dispose()
        {
            _serverManager.Dispose();
        }

        #endregion

        #region property

        private readonly ServerManager _serverManager;

        #endregion

        #region method

        public List<string> GetWebsiteList()
        {
            try
            {
                return _serverManager.Sites.Select(site => site.Name).ToList();
            }
            catch (Exception ex)
            {
                return new List<string> {$"fail: {ex.Message}"};
            }
        }

        public string CreateWebsite(string websiteName, string websitePhysicalPath, string applicationPoolName)
        {
            try
            {
                if (_serverManager.Sites.Any(site => site.Name == websiteName)) return $"fail: {websiteName} exists.";
                //_serverManager.Sites.Add(websiteName, "http", "*.80", websitePhysicalPath);
                var website = _serverManager.Sites.Add(websiteName, websitePhysicalPath, 80);
                website.ApplicationDefaults.ApplicationPoolName = applicationPoolName;
                foreach (var application in website.Applications) application.ApplicationPoolName = applicationPoolName;
                _serverManager.CommitChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return $"fail: {ex.Message}";
            }
        }

        public string CreateWebsiteApplication(string websiteName, string applicationPath, string applicationPhysicalPath, string applicationPoolName)
        {
            try
            {
                var website = _serverManager.Sites.FirstOrDefault(site => site.Name == websiteName);
                if (website == null) return $"fail: {websiteName} not exist.";
                if (website.Applications.Any(application => application.Path == applicationPath)) return $"fail: {applicationPath} exists.";
                var websiteApplication = website.Applications.Add(applicationPath, applicationPhysicalPath);
                websiteApplication.ApplicationPoolName = applicationPoolName;
                _serverManager.CommitChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return $"fail: {websiteName}, {applicationPath}: {ex.Message}";
            }
        }

        #endregion
    }
}
