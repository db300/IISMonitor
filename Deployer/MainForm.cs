using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deployer
{
    public partial class MainForm : Form
    {
        #region constructor
        public MainForm()
        {
            InitializeComponent();

            InitUi();
        }
        #endregion

        #region method
        private void WriteConnectionStrings(BackgroundWorker work)
        {
            /*
            var dict = new Dictionary<string, string>();
            var connectionStrings = Resources.ConnectionStrings.Split('\n').Select(s => s.Trim()).ToList();
            foreach (var connectionString in connectionStrings)
            {
                var key = connectionString.Substring(0, connectionString.IndexOf('='));
                var value = connectionString.Substring(connectionString.IndexOf('=') + 1);
                dict.Add(key, value);
            }

            using (var webConfigManager = new iHawkIISLibrary.WebConfigManager())
            {
                var s = webConfigManager.AddConnectionStrings("Default Web Site", dict, true);
                work.ReportProgress(0, s == "success" ? "INFO: connectionStrings success" : "ERROR: connectionStrings fail");
            }
            */
        }
        #endregion

        #region ui
        private void InitUi()
        {
            ClientSize = new Size(1000, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "服务部署器";

            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Parent = this
            };
            var tpDeploy = new TabPage("批量部署") { BorderStyle = BorderStyle.None, Name = "tpDeploy" };
            var tpDbConnectionString = new TabPage("关系型数据库连接串管理") { BorderStyle = BorderStyle.None, Name = "tpDbConnectionString" };
            var tpAppSetting = new TabPage("应用程序设置管理") { BorderStyle = BorderStyle.None, Name = "tpAppSetting" };
            tabControl.TabPages.AddRange(new[]
            {
                tpDeploy ,
                tpDbConnectionString,
                tpAppSetting
            });
            //批量部署
            var deployModule = new Modules.DeployModule { Dock = DockStyle.Fill, Parent = tpDeploy };
            //关系型数据库连接串管理
            var connectionStringModule = new Modules.ConnectionStringModule { Dock = DockStyle.Fill, Parent = tpDbConnectionString };
            //应用程序设置管理
            var appSettingModule = new Modules.AppSettingModule { Dock = DockStyle.Fill, Parent = tpAppSetting };
        }
        #endregion
    }
}
