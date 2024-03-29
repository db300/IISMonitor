﻿using System;
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

        public string AddConnectionStrings(string websiteName, Dictionary<string, string> nameConnectionStringPair, bool clear)
        {
            try
            {
                var config = _serverManager.GetWebConfiguration(websiteName, "");
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

        public string AddAppSettings(string websiteName, Dictionary<string, string> keyValuePair, bool clear)
        {
            try
            {
                var config = _serverManager.GetWebConfiguration(websiteName, "");
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
