using Newtonsoft.Json;
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

namespace Deployer.Modules
{
    /// <summary>
    /// 批量部署
    /// </summary>
    public partial class DeployModule : UserControl
    {
        #region constructor
        public DeployModule()
        {
            InitializeComponent();

            InitUi();

            Load += DeployModule_Load;
        }
        #endregion

        #region property
        private TextBox _txtReleasePackDir;
        private TextBox _txtReleaseUnpackDir;
        private TextBox _txtLog;

        private DeployConfigItem _deployConfigItem;
        #endregion

        #region event handler
        private void DeployModule_Load(object sender, EventArgs e)
        {
            using (var sr = new StreamReader("deployconfig.json"))
            {
                var json = sr.ReadToEnd();
                _deployConfigItem = JsonConvert.DeserializeObject<DeployConfigItem>(json);
                _deployConfigItem.ReleasePackDir = $@"{Application.StartupPath}\{_deployConfigItem.ReleasePackDir}\";
                _deployConfigItem.ReleaseUnpackDir = $@"{Application.StartupPath}\{_deployConfigItem.ReleaseUnpackDir}\";
                if (!Directory.Exists(_deployConfigItem.ReleasePackDir)) Directory.CreateDirectory(_deployConfigItem.ReleasePackDir);
                if (!Directory.Exists(_deployConfigItem.ReleaseUnpackDir)) Directory.CreateDirectory(_deployConfigItem.ReleaseUnpackDir);
                _txtReleasePackDir.Text = _deployConfigItem.ReleasePackDir;
                _txtReleaseUnpackDir.Text = _deployConfigItem.ReleaseUnpackDir;
            }
        }

        private void BtnDeploy_Click(object sender, EventArgs e)
        {
            var background = new BackgroundWorker { WorkerReportsProgress = true };
            background.DoWork += (work, ee) =>
            {
                //unpack
                Unpack((BackgroundWorker)work);
                //create app pool
                CreateAppPool((BackgroundWorker)work);
                //创建应用程序
                CreateApp((BackgroundWorker)work);
                //写入配置
                //WriteAppSettings((BackgroundWorker)work);
                //写入连接字符串
                //WriteConnectionStrings((BackgroundWorker)work);
                /*
                //连接数据库建库
                if (Config.EnableCreateDatabase) CreateDatabase((BackgroundWorker)work, Config.ConnectionString);
                //连接数据库建表
                if (Config.EnableCreateTable) CreateTable((BackgroundWorker)work, Config.ConnectionString);
                */

                ((BackgroundWorker)work).ReportProgress(0, "INFO: 服务创建完成");
            };
            background.ProgressChanged += (work, ee) => { _txtLog.AppendText($"【{DateTime.Now}】{ee.UserState}\r\n"); };
            background.RunWorkerCompleted += (work, ee) => { };
            background.RunWorkerAsync();
            ((Button)sender).Enabled = false;
            _txtLog.AppendText($"【{DateTime.Now}】INFO: deploy start...\r\n");
        }
        #endregion

        #region method
        private void Unpack(BackgroundWorker work)
        {
            var zipFiles = Directory.GetFiles(_deployConfigItem.ReleasePackDir, "*.zip").ToList();
            if (!(zipFiles?.Count > 0))
            {
                work.ReportProgress(0, "ERROR: unzip fail, no zip file");
                return;
            }
            foreach (var file in zipFiles)
            {
                work.ReportProgress(0, $"INFO: unpacking {file}");
                var s = iHawkZipStdLibrary.SharpZip.DecomparessFile(file, _deployConfigItem.ReleaseUnpackDir);
                work.ReportProgress(0, string.IsNullOrWhiteSpace(s) ? $"INFO: {file} unpack done." : $"ERROR: {file} unzip fail, {s}");
            }
            work.ReportProgress(0, "INFO: release packages unpack done.");
        }

        private void CreateAppPool(BackgroundWorker work)
        {
            using (var appPoolsManager = new iHawkIISLibrary.ApplicationPoolsManager())
            {
                foreach (var directoryInfo in new DirectoryInfo(_deployConfigItem.ReleaseUnpackDir).GetDirectories())
                {
                    try
                    {
                        var appPoolName = $"{_deployConfigItem.AppPoolNamePrefix}{directoryInfo.Name}{_deployConfigItem.AppPoolNameSuffix}";
                        var runtimeVersion = directoryInfo.GetFiles(_deployConfigItem.NetTagFileName).Length > 0 ? "" : "v4.0";
                        work.ReportProgress(0, $"INFO: create application pool {appPoolName} {appPoolsManager.CreateApplicationPool(appPoolName, runtimeVersion)}");
                    }
                    catch (Exception ex)
                    {
                        work.ReportProgress(0, $"ERROR: {directoryInfo.Name} Application Pool create: {ex.Message}");
                    }
                }
                work.ReportProgress(0, "INFO: application pools create done.");
            }
        }

        private void CreateApp(BackgroundWorker work)
        {
            using (var websitesManager = new iHawkIISLibrary.WebsitesManager())
            {
                var websites = websitesManager.GetWebsiteList();
                if (websites.Count == 0)
                {
                    work.ReportProgress(0, "ERROR: 默认网站不存在");
                    return;
                }

                var website = websites[0];
                foreach (var directoryInfo in new DirectoryInfo(_deployConfigItem.ReleaseUnpackDir).GetDirectories())
                {
                    try
                    {
                        var appPoolName = $"{_deployConfigItem.AppPoolNamePrefix}{directoryInfo.Name}{_deployConfigItem.AppPoolNameSuffix}";
                        var virtualPath = $"/{directoryInfo.Name}";
                        work.ReportProgress(0, $"INFO: create application {virtualPath} {websitesManager.CreateWebsiteApplication(website, virtualPath, directoryInfo.FullName, appPoolName)}");
                    }
                    catch (Exception ex)
                    {
                        work.ReportProgress(0, $"ERROR: {directoryInfo.Name} Application create: {ex.Message}");
                    }
                }
                work.ReportProgress(0, "INFO: applications create done.");
            }
        }
        #endregion

        #region ui
        private void InitUi()
        {
            _txtReleasePackDir = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.White,
                Font = new Font(Font.FontFamily, 12),
                Location = new Point(20, 20),
                Parent = this,
                ReadOnly = true,
                Width = this.ClientSize.Width - 40
            };
            _txtReleaseUnpackDir = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.White,
                Font = new Font(Font.FontFamily, 12),
                Location = new Point(_txtReleasePackDir.Left, _txtReleasePackDir.Bottom + 12),
                Parent = this,
                ReadOnly = true,
                Width = _txtReleasePackDir.Width
            };
            var btnDeploy = new Button
            {
                AutoSize = true,
                Location = new Point(_txtReleasePackDir.Left, _txtReleaseUnpackDir.Bottom + 12),
                Parent = this,
                Text = "开始部署"
            };
            btnDeploy.Click += BtnDeploy_Click;
            _txtLog = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = Color.White,
                Font = new Font(Font.FontFamily, 12),
                Location = new Point(_txtReleasePackDir.Left, btnDeploy.Bottom + 12),
                Multiline = true,
                Parent = this,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 20 - btnDeploy.Bottom - 12),
                WordWrap = false
            };
        }
        #endregion
    }
}
