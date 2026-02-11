using IISMonitor.Properties;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IISMonitor
{
    public partial class MainForm : Form
    {
        #region constructor
        public MainForm()
        {
            InitializeComponent();

            InitUi();

            Load += MainForm_Load;
        }
        #endregion

        #region property
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private FlowLayoutPanel _panel;
        private TextBox _txtLog4Opera;
        private TextBox _txtLog4Monitor;

        private const string startupConfigFile = "startup.ini";
        private readonly List<string> _startupList = new List<string>();
        #endregion

        #region method
        private void RefreshPoolList()
        {
            var poolList = AppSingleton.Apm.GetApplicationPoolList();
            poolList.Sort();
            Logger.Info("获取到 {0} 个应用程序池", poolList.Count);
            foreach (var pool in poolList)
            {
                var appPoolPanel = new AppPoolPanel
                {
                    Margin = new Padding(0),
                    Padding = new Padding(0),
                    Parent = _panel,
                    Width = 330
                };
                appPoolPanel.Notification += (ap, tt, ss) =>
                {
                    switch (tt)
                    {
                        case "Monitor":
                            _txtLog4Monitor.AppendText($"【{DateTime.Now}】{ss}\r\n");
                            break;
                        case "Opera":
                            _txtLog4Opera.AppendText($"【{DateTime.Now}】{ss}\r\n");
                            break;
                    }
                };
                appPoolPanel.Update(pool, _startupList.Contains(pool));
                _panel.Controls.Add(appPoolPanel);
            }
        }
        #endregion

        #region event handler
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(startupConfigFile))
            {
                using (var sr = new StreamReader(startupConfigFile, Encoding.Unicode))
                {
                    var lines = sr.ReadToEnd().Split('\n').Select(a => a.Trim()).ToList();
                    lines.RemoveAll(a => string.IsNullOrWhiteSpace(a));
                    _startupList.AddRange(lines);
                }
                Logger.Info("加载启动配置，自动监测列表：{0}", string.Join(", ", _startupList));
            }
            else
            {
                Logger.Warn("启动配置文件 {0} 不存在", startupConfigFile);
            }

            try
            {
                RefreshPoolList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "刷新应用程序池列表失败");
            }
        }
        #endregion

        #region ui
        private void InitUi()
        {
            Icon = Resources.Icons_Land_Vista_Hardware_Devices_Home_Server;
            StartPosition = FormStartPosition.CenterScreen;
            Text = $"IISMonitor {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";

            var panel0 = new Panel
            {
                Dock = DockStyle.Left,
                Parent = this,
                Width = 350
            };
            _panel = new FlowLayoutPanel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(0),
                Parent = panel0
            };
            _panel.BringToFront();

            var panel1 = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                Parent = this
            };
            panel1.BringToFront();
            _txtLog4Opera = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                Parent = panel1.Panel1,
                ReadOnly = true
            };
            _txtLog4Monitor = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                Parent = panel1.Panel2,
                ReadOnly = true
            };
        }
        #endregion
    }
}
