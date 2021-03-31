using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private readonly Timer _timer = new Timer {Interval = 1000, Enabled = false};

        private string _appPoolName;

        private TextBox _txtAppPool;

        #endregion

        #region method

        public void Update(string name)
        {
            _appPoolName = name;
            _txtAppPool.Text = name;
            _timer.Tick += (tt, ee) =>
            {
                var s = AppSingleton.Apm.CheckAndRestart(name);
                if (!string.IsNullOrWhiteSpace(s)) Notification?.Invoke(this, "Monitor", s);
            };
        }

        #endregion

        #region event handler

        private void BtnMonitor_Click(object sender, EventArgs e)
        {
            var buttonText = ((Button) sender).Text;
            switch (buttonText)
            {
                case "监测":
                    _timer.Enabled = true;
                    ((Button) sender).Text = @"停止";
                    Notification?.Invoke(this, "Opera", $"开启监测：{_appPoolName}");
                    break;
                case "停止":
                    _timer.Enabled = false;
                    ((Button) sender).Text = @"监测";
                    Notification?.Invoke(this, "Opera", $"停止监测：{_appPoolName}");
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

            var btnMonitor = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Parent = this,
                Text = @"监测"
            };
            btnMonitor.Location = new Point(ClientSize.Width - 10 - btnMonitor.Width, (ClientSize.Height - btnMonitor.Height) / 2);
            btnMonitor.Click += BtnMonitor_Click;

            _txtAppPool = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Left = 10,
                Parent = this,
                ReadOnly = true,
                Width = ClientSize.Width - 40 - btnMonitor.Left
            };
            _txtAppPool.Location = new Point(10, (ClientSize.Height - _txtAppPool.Height) / 2);
        }

        #endregion
    }
}
