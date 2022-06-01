using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;

namespace iHawkIISLibrary
{
    /// <summary>
    /// 应用程序池管理器
    /// </summary>
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
                return new List<string> { ex.Message };
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

        /// <summary>
        /// 创建应用程序池
        /// </summary>
        /// <param name="name">应用程序池名称</param>
        /// <param name="managedRuntimeVersion">The version number of the .NET Framework that is used by the application pool. The default is "v4.0". 如果创建无托管代码，该参数为""</param>
        public string CreateApplicationPool(string name, string managedRuntimeVersion = "v4.0")
        {
            try
            {
                if (_serverManager.ApplicationPools.Any(applicationPool => applicationPool.Name == name)) return $"fail: {name} exists.";
                var appPool = _serverManager.ApplicationPools.Add(name);
                appPool.ManagedRuntimeVersion = managedRuntimeVersion;
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

        /// <summary>
        /// 监测应用程序池并重启（如果停止）
        /// </summary>
        /// <param name="name">应用程序池名称</param>
        public string CheckAndRestart(string name)
        {
            try
            {
                if (GetApplicationPoolState(name) == ObjectState.Started) return "";
                return $"重启 {name}：{StartApplicationPool(name)}";
            }
            catch (Exception ex)
            {
                return $"{name} 监测或重启失败：{ex.Message}";
            }
        }

        #endregion
    }
}
