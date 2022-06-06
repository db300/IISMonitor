using System;
using System.Collections.Generic;
using Microsoft.Web.Administration;

namespace iHawkIISLibrary
{
    /// <summary>
    /// WebConfig管理器
    /// </summary>
    public class WebConfigManager : IDisposable
    {
        #region constructor

        public WebConfigManager()
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

        [Obsolete]
        public void Test()
        {
            var config = _serverManager.GetWebConfiguration("Default Web Site", "");
            var section = config.GetSection("connectionStrings");
            var collection = section.GetCollection();
            var element = collection.CreateElement("add");
            element["connectionString"] = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            element["name"] = "hellofont";
            collection.Add(element);
            _serverManager.CommitChanges();
        }

        public Dictionary<string, string> GetConnectionStrings(string websiteName, string virtualPath)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                var config = _serverManager.GetWebConfiguration(websiteName, virtualPath);
                var section = config.GetSection("connectionStrings");
                var collection = section.GetCollection();
                foreach (var item in collection)
                {
                    dict.Add(item["name"].ToString(), item["connectionString"].ToString());
                }
                return dict;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public string AddConnectionStrings(string websiteName, string virtualPath, Dictionary<string, string> nameConnectionStringPair, bool clear)
        {
            try
            {
                var config = _serverManager.GetWebConfiguration(websiteName, virtualPath);
                var section = config.GetSection("connectionStrings");
                var collection = section.GetCollection();
                if (clear) collection.Clear();
                foreach (var pair in nameConnectionStringPair)
                {
                    var element = collection.CreateElement("add");
                    element["name"] = pair.Key;
                    element["connectionString"] = pair.Value;
                    collection.Add(element);
                }

                _serverManager.CommitChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Dictionary<string, string> GetAppSettings(string websiteName, string virtualPath)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                var config = _serverManager.GetWebConfiguration(websiteName, virtualPath);
                var section = config.GetSection("appSettings");
                var collection = section.GetCollection();
                foreach (var item in collection)
                {
                    dict.Add(item["key"].ToString(), item["value"].ToString());
                }
                return dict;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public string GetAppSettingsJsonFileName(string websiteName, string virtualPath)
        {
            try
            {
                var physicalPath = _serverManager.Sites[websiteName].Applications[virtualPath].VirtualDirectories[0].PhysicalPath;
                return physicalPath.EndsWith("\\") ? $"{physicalPath}appsettings.json" : $@"{physicalPath}\appsettings.json";
            }
            catch (Exception ex)
            {
                return $"fail: {ex.Message}";
            }
        }

        public string AddAppSettings(string websiteName, string virtualPath, Dictionary<string, string> keyValuePair, bool clear)
        {
            try
            {
                var config = _serverManager.GetWebConfiguration(websiteName, virtualPath);
                var section = config.GetSection("appSettings");
                var collection = section.GetCollection();
                if (clear) collection.Clear();
                foreach (var pair in keyValuePair)
                {
                    var element = collection.CreateElement("add");
                    element["key"] = pair.Key;
                    element["value"] = pair.Value;
                    collection.Add(element);
                }

                _serverManager.CommitChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}
