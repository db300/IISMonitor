using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;

namespace iHawkIISLibrary
{
    public class ApplicationPoolsManager : IDisposable
    {
        #region constructor

        public ApplicationPoolsManager()
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
        public static string[] Test()
        {
            var list = new List<string>();
            using (var serverManager = new ServerManager())
            {
                list.AddRange(serverManager.ApplicationPools.Select(applicationPool => $"{applicationPool.Name}: {applicationPool.State}"));
            }

            return list.ToArray();
        }

        public List<string> GetApplicationPoolList()
        {
            try
            {
                return _serverManager.ApplicationPools.Select(applicationPool => applicationPool.Name).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<string> {ex.Message};
            }
        }

        public ApplicationPool GetApplicationPool(string applicationPoolName)
        {
            return _serverManager.ApplicationPools[applicationPoolName];
        }

        public ApplicationPool GetApplicationPool(int index)
        {
            return _serverManager.ApplicationPools[index];
        }

        public ObjectState GetApplicationPoolState(string applicationPoolName)
        {
            return _serverManager.ApplicationPools[applicationPoolName].State;
        }

        public ObjectState GetApplicationPoolState(int index)
        {
            return _serverManager.ApplicationPools[index].State;
        }

        public ObjectState StartApplicationPool(string applicationPoolName)
        {
            return _serverManager.ApplicationPools[applicationPoolName].Start();
        }

        public ObjectState StartApplicationPool(int index)
        {
            return _serverManager.ApplicationPools[index].Start();
        }

        public ObjectState StopApplicationPool(string applicationPoolName)
        {
            return _serverManager.ApplicationPools[applicationPoolName].Stop();
        }

        public ObjectState StopApplicationPool(int index)
        {
            return _serverManager.ApplicationPools[index].Stop();
        }

        public string CreateApplicationPool(string name)
        {
            try
            {
                if (_serverManager.ApplicationPools.Any(applicationPool => applicationPool.Name == name)) return $"fail: {name} exists.";
                var appPool = _serverManager.ApplicationPools.Add(name);
                appPool.ManagedRuntimeVersion = "v4.0";
                appPool.Enable32BitAppOnWin64 = false;
                appPool.ManagedPipelineMode = ManagedPipelineMode.Integrated;
                _serverManager.CommitChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return $"fail: {name} {ex.Message}";
            }
        }

        #endregion
    }
}
