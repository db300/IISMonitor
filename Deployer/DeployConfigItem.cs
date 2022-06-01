using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer
{
    internal class DeployConfigItem
    {
        /// <summary>
        /// 发布包存放目录
        /// </summary>
        public string ReleasePackDir { get; set; }

        /// <summary>
        /// 发布包解压目录
        /// </summary>
        public string ReleaseUnpackDir { get; set; }

        /// <summary>
        /// 应用程序池名称前缀
        /// </summary>
        public string AppPoolNamePrefix { get; set; }

        /// <summary>
        /// 应用程序池名称后缀
        /// </summary>
        public string AppPoolNameSuffix { get; set; }

        /// <summary>
        /// .net5/6 标识文件名
        /// </summary>
        public string NetTagFileName { get; set; }
    }
}
