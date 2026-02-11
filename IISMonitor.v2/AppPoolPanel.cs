using System;
using System.Drawing;
using System.Windows.Forms;
using NLog;

namespace IISMonitor
{
    public partial class AppPoolPanel : UserControl
    {
        #region constructor
        public AppPoolPanel()
        {
            InitializeComponent();

            InitUi();
        }
        #endregion

        #region property
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Timer _timer = new Timer { Interval = 1000, Enabled = false };

        private string _appPoolName;

        private TextBox _txtAppPool;
        private Button _btnMonitor;
        #endregion

        #region method
        public void Update(string name, bool startup)
        {
            _appPoolName = name;
            _txtAppPool.Text = name;
            _timer.Tick += (tt, ee) =>
            {
                try
                {
                    var s = AppSingleton.Apm.CheckAndRestart(name);
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        Logger.Warn("应用程序池 {0} 状态异常，执行重启：{1}", name, s);
                        Notification?.Invoke(this, "Monitor", s);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "监测应用程序池 {0} 时发生异常", name);
                }
            };
            if (startup) Startup();
        }

        private void Startup()
        {
            _timer.Enabled = true;
            _btnMonitor.Text = @"停止";
            _btnMonitor.BackColor = Color.LimeGreen;
            Logger.Info("开启监测：{0}", _appPoolName);
            Notification?.Invoke(this, "Opera", $"开启监测：{_appPoolName}");
        }

        private void Stop()
        {
            _timer.Enabled = false;
            _btnMonitor.Text = @"监测";
            _btnMonitor.BackColor = SystemColors.Control;
            Logger.Info("停止监测：{0}", _appPoolName);
            Notification?.Invoke(this, "Opera", $"停止监测：{_appPoolName}");
        }
        #endregion

        #region event handler
        private void BtnMonitor_Click(object sender, EventArgs e)
        {
            var buttonText = ((Button)sender).Text;
            switch (buttonText)
            {
                case "监测":
                    Startup();
                    break;
                case "停止":
                    Stop();
                    break;
            }
        }

        public delegate void NotificationHandler(object sender, string infoType, string info);
        public event NotificationHandler Notification;
        #endregion

        #region ui
        private void InitUi()
        {
            Height = 25;

            _btnMonitor = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                BackColor = SystemColors.Control,
                Parent = this,
                Text = @"监测"
            };
            _btnMonitor.Location = new Point(ClientSize.Width - 10 - _btnMonitor.Width, (ClientSize.Height - _btnMonitor.Height) / 2);
            _btnMonitor.Click += BtnMonitor_Click;

            _txtAppPool = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Left = 10,
                Parent = this,
                ReadOnly = true,
                Width = ClientSize.Width - 40 - _btnMonitor.Left
            };
            _txtAppPool.Location = new Point(10, (ClientSize.Height - _txtAppPool.Height) / 2);
        }
        #endregion
    }
}
